using Dnd.FileData;
using Dnd.Gameplay;
using Dnd.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Dnd.Game
{
    public class GameController : MonoBehaviour
    {
        public GameView gameView;

        [SerializeField]
        private GameObject _characterPrefab;
        [SerializeField]
        private RectTransform _charactersPlace;
        [SerializeField]
        private GameObject _itemPrefab;
        [SerializeField]
        private RectTransform _listContent;

        private UDPController _udpController;
        private float _stepDelay = 0.12f;
        private float _deltaY = 20f;
        private float _itemDelta = 10f;
        private bool _menuState = true;

        public void Init(string _hostName)
        {
            List<PlayingCharacter> characters = GameDataContainer.Instance.playersData;
            StartCoroutine(CreateCharacters(characters));
            _udpController = new UDPController();
            _udpController.Init(_hostName);
        }

        /// <summary>
        /// button onclick event in Unity inspector
        /// </summary>
        /// <param name="_index"></param>
        public void OpenInfoBlock(int _index)
        {
            SwitchBlocks(_index);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_menuState)
                    Application.Quit();
                else
                {
                    _menuState = true;
                    gameView.Back();
                }
            }

            _udpController.Execute();
        }

        private void OpenCharacter(PlayingCharacter _character)
        {
            if (gameView != null)
            {
                _menuState = false;
                gameView.SelectCharacter(_character);
                GameDataContainer.Instance.currentCharacter = _character;
                _menuState = false;
            }
        }

        private void SwitchBlocks(int _index)
        {
            PlayingCharacter current = GameDataContainer.Instance.currentCharacter;
            List<Gameplay.CharacterInfo> info;

            if (_index == 0)
                info = GetInfo(current.race.raceSkills);
            else if (_index == 1)
                info = GetInfo(current.playerLevel.classSkills);
            else if (_index == 2)
                info = GetInfo(current.traits);
            else
                info = GetInfo(current.spells);

            Vector3 startPos = new Vector3(-_listContent.rect.width / 2f + 30f, -_itemDelta, 0f);
            _listContent.sizeDelta = new Vector2(_listContent.sizeDelta.x, _itemDelta + info.Count * (96f + _itemDelta) + 20f);

            List<GameObject> items = new List<GameObject>();

            for (int i = 0; i < info.Count; i++)
            {
                GameObject itemGO = Instantiate(_itemPrefab);
                RectTransform itemRect = itemGO.transform as RectTransform;
                itemRect.SetParent(_listContent, false);
                itemRect.localPosition = startPos + Vector3.down * (itemRect.sizeDelta.y + _itemDelta) * i;
                Button btn = itemRect.GetComponent<Button>();
                int itemId = i;
                string description = info[i].description;
                btn.onClick.AddListener(delegate { OpenDescription(itemId, description); });
                items.Add(itemGO);
            }

            gameView.SelectBlock(info, items, _index);
        }

        private List<Gameplay.CharacterInfo> GetInfo<T>(List<T> _skills) where T : Gameplay.CharacterInfo
        {
            List<Gameplay.CharacterInfo> info = new List<Gameplay.CharacterInfo>();
            for (int i = 0; i < _skills.Count; i++)
                info.Add(_skills[i]);

            return info;
        }

        private void OpenDescription(int _id, string _text)
        {
            gameView.csView.OpenDescription(_id, _text);
        }

        private void OnApplicationQuit()
        {
            _udpController.OnApplicationQuit();
        }

        private IEnumerator CreateCharacters(List<PlayingCharacter> _characters)
        {
            Sprite[] iconsAtlas = Resources.LoadAll<Sprite>("Sprites/characters");
            float itemSize = 0f;

            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < _characters.Count; i++)
            {
                GameObject chGO = Instantiate(_characterPrefab);
                RectTransform chRect = chGO.transform as RectTransform;
                chRect.SetParent(_charactersPlace, false);
                if (itemSize <= 0f)
                    itemSize = chRect.sizeDelta.y + _deltaY * 2;
                chRect.localPosition = Vector3.down * itemSize * i;
                Button btn = chGO.GetComponent<Button>();
                Image icon = chRect.GetChild(0).GetComponent<Image>();
                Text text = chRect.GetChild(1).GetComponent<Text>();
                Text level = chRect.GetChild(2).GetComponentInChildren<Text>();

                PlayingCharacter current = _characters[i];
                btn.onClick.AddListener(delegate { OpenCharacter(current); });
                Sprite avatarSprite = iconsAtlas.Single(s => s.name == _characters[i].icon);
                icon.sprite = avatarSprite;
                CharacterClass baseClass = _characters[i].playerLevel.currentClasses.Keys.ToList()[0];
                text.text = string.Format("<b>{0}</b>\r\n<i><size=48>{1}\r\n{2}</size></i>", _characters[i].characterName, _characters[i].race, baseClass.classType);
                level.text = _characters[i].playerLevel.generalLevel.ToString();

                yield return new WaitForSeconds(_stepDelay);
            }

            _charactersPlace.sizeDelta = new Vector2(_charactersPlace.sizeDelta.x, _characters.Count * itemSize - _deltaY);
            RectTransform maskRect = transform as RectTransform;
            if (_charactersPlace.sizeDelta.y > maskRect.rect.height)
                gameView.ViewCharacters();
        }
    }
}