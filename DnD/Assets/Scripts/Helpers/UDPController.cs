using Dnd.FileData;
using Dnd.Game;
using System;
using UnityEngine;

namespace Dnd.Helpers
{
    public class UDPController
    {
        public delegate void OnCheck(int _index, string _res);
        public event OnCheck onCheck;

        #region private vars
        private int _portRead = 5187;
        private int _portWrite = 5188;
        private UDPReceiver _receiver;
        private bool _inited = false;
        #endregion

        #region public functions
        public void Init(string _hostName)
        {
            _inited = true;
            _receiver = new UDPReceiver(_hostName, _portRead, _portWrite);
        }

        public void ReactToCommand()
        {
            if (string.IsNullOrEmpty(_receiver.message)) return;

            string[] splitData = _receiver.message.Split('_');
            if (splitData[0].CompareTo("add") == 0)
            {
                if (splitData[1].CompareTo("player") == 0)
                {
                    try
                    {
                        PlayerData data = GameDataLoader.LoadJson<PlayerData>(splitData[2]);
                        GameDataContainer.Instance.AddPlayingCharacter(data);
                    }
                    catch (Exception)
                    {
                        Debug.Log("Can't parse data: " + _receiver.message);
                    }
                }
                else if (splitData[1].CompareTo("enemy") == 0)
                {
                    try
                    {
                        int distance = int.Parse(splitData[2]);
                        EnemyData data = GameDataLoader.LoadJson<EnemyData>(splitData[3]);
                        GameDataContainer.Instance.AddWorldCharacter(data, distance);
                    }
                    catch (Exception)
                    {
                        Debug.Log("Can't parse data: " + _receiver.message);
                    }
                }
            }
            else if (splitData[0].CompareTo("check") == 0)
            {
                if (splitData[1].CompareTo("skill") == 0)
                {
                    try
                    {
                        int index = int.Parse(splitData[2]);
                        int rollValue = int.Parse(splitData[3]);
                        foreach (var ch in GameDataContainer.Instance.allCharacters)
                        {
                            bool result = ch.RollCheckSkill(index, rollValue);
                            string checkStatus = (result) ? "Проверка пройдена" : "Провал в проверка";
                            //next this result goes to delegate
                            //onCheck?.Invoke(index, result);
                        }
                    }
                    catch (Exception)
                    {
                        Debug.Log("Can't parse data: " + _receiver.message);
                    }
                }
            }
        }
        #endregion

        public void Execute()
        {
            if (_inited && _receiver.isEmail)
            {
                ReactToCommand();
                _receiver.message = string.Empty;
                _receiver.isEmail = false;
            }
        }

        public void OnApplicationQuit()
        {
            if (_inited)
                _receiver.CloseThread();
        }
    }
}