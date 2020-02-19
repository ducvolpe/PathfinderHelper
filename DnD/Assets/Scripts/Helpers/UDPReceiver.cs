using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dnd.Helpers
{
    public class UDPReceiver
    {
        #region public vars
        public delegate string OnCollectInfo();
        public OnCollectInfo onCollectInfo;
        public string message { get; set; }
        public bool isEmail { get; set; }
        #endregion

        #region private vars
        private UdpClient _client;
        private CancellationTokenSource _CTS;
        private int _portRead;
        private string _messageWrite;
        private bool _isAlive = false;
        private string _checkQuestion = "checkstatus";
        private string _checkAnswer = "checkok";
        private string _okAnswer = "message received";
        #endregion

        #region public functions
        public UDPReceiver(string _host, int _readPort, int _sendPort)
        {
            this._portRead = _readPort;
            this._isAlive = true;
            _client = new UdpClient();
            _client.Connect(_host, _sendPort);
            var t = Task.Run(StartReaderAsync);
        }

        public void SendMessage(string _message)
        {
            if (string.IsNullOrEmpty(_message))
                return;

            _messageWrite = _message;
            var t = Task.Run(StartSendingMessage);
        }

        public void CloseThread()
        {
            if (_isAlive)
            {
                _isAlive = false;
                if (_CTS != null)
                    _CTS.Cancel();

                if (_client != null)
                    _client.Close();
            }
        }
        #endregion

        #region private functions
        private async Task StartSendingMessage()
        {
            var requestPacket = Encoding.ASCII.GetBytes(_messageWrite);
            await _client.SendAsync(requestPacket, requestPacket.Length).WithCancellation(_CTS.Token);
        }

        private async Task StartReaderAsync()
        {
            await ReaderAsync().ConfigureAwait(false);
        }

        private async Task ReaderAsync()
        {
            using (var client = new UdpClient(_portRead))
            {
                _CTS = new CancellationTokenSource();
                var ep = new IPEndPoint(IPAddress.Any, _portRead);

                do
                {
                    try
                    {
                        byte[] datagram = client.Receive(ref ep);
                        if (datagram.Length > 0)
                        {
                            var answer = DecipherMessage(datagram);

                            if (answer.CompareTo(_checkQuestion) == 0)
                            {
                                var requestPacket = Encoding.ASCII.GetBytes(_checkAnswer);
                                await _client.SendAsync(requestPacket, requestPacket.Length).WithCancellation(_CTS.Token);
                            }
                            else if (!string.IsNullOrEmpty(answer))
                            {
                                message = answer;
                                isEmail = true;
                                var requestPacket = Encoding.ASCII.GetBytes(_okAnswer);
                                await _client.SendAsync(requestPacket, requestPacket.Length).WithCancellation(_CTS.Token);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        client.Close();
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.Log("Exception: " + e.Message + ";");
                    }
                } while (_isAlive);
                
                _CTS = null;
            }
        }

        private string DecipherMessage(byte[] _data)
        {
            string text = Encoding.UTF8.GetString(_data);
            if (!string.IsNullOrEmpty(text)) return text;
            else return string.Empty;
        }
        #endregion
    }

    public static class AsyncExtensions
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return task.Result;
        }
    }
}