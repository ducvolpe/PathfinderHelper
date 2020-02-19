using Dnd.FileData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Dnd.Game
{
    public class GameDataLoader : MonoBehaviour
    {
        [SerializeField]
        private string _characterUrl;
        [SerializeField]
        private string _equipmentUrl;
        [SerializeField]
        private GameObject _loadingIcon;
        [SerializeField]
        private GameObject _warningText;
        [SerializeField]
        private GameController _gameController;

        private string _hostName; //we can change it using InputField

        public static T LoadJson<T>(string _text)
        {
            T items = JsonUtility.FromJson<T>(_text);
            return items;
        }

        private void Start()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _warningText.SetActive(true);
                _loadingIcon.SetActive(false);
            }
            else
                StartCoroutine(LoadEquipmentData());
        }

        private IEnumerator LoadEquipmentData()
        {
            UnityWebRequest www = UnityWebRequest.Get(_equipmentUrl);
            yield return www.SendWebRequest();

            if (!www.isNetworkError && !www.isHttpError)
            {
                string jsonData = www.downloadHandler.text;
                StuffContent items = LoadJson<StuffContent>(jsonData);
                for (int i = 0; i < items.meleeWeapon.Count; i++)
                    items.allEquipment.Add(items.meleeWeapon[i]);

                for (int i = 0; i < items.distanceWeapon.Count; i++)
                    items.allEquipment.Add(items.distanceWeapon[i]);

                StartCoroutine(LoadCharactersData(items.allEquipment));
            }
            else
            {
                Destroy(_loadingIcon);
                _warningText.SetActive(true);
            }
        }

        private IEnumerator LoadCharactersData(List<EquipmentData> _equipment)
        {
            UnityWebRequest www = UnityWebRequest.Get(_characterUrl);
            yield return www.SendWebRequest();

            Destroy(_loadingIcon);

            if (!www.isNetworkError && !www.isHttpError)
            {
                string jsonData = www.downloadHandler.text;
                Content content = LoadJson<Content>(jsonData);
                for (int i = 0; i < content.playingCharacters.Count; i++)
                {
                    content.playingCharacters[i].equipment = new List<EquipmentData>();
                    for (int j = 0; j < content.playingCharacters[i].equipmentIds.Count; j++)
                    {
                        EquipmentData eData = _equipment.Find(x => x.id == content.playingCharacters[i].equipmentIds[j]);
                        if (eData != null)
                            content.playingCharacters[i].equipment.Add(eData);
                    }
                }

                GameDataContainer.Instance.StartGame(content.playingCharacters);
                _gameController.Init(_hostName);
            }
            else
                _warningText.SetActive(true);
        }
    }
}