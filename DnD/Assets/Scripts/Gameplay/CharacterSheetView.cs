using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Dnd.Gameplay;

namespace Dnd.Game
{
    public class CharacterSheetView : MonoBehaviour
    {
        [SerializeField]
        private Text _characterName;
        [SerializeField]
        private Text _characterLevel;
        [SerializeField]
        private Text _race;
        [SerializeField]
        private Animator _menuAnim;

        [SerializeField]
        private Animator[] _blocks;
        [SerializeField]
        private Text _descriptionContent;
        [SerializeField]
        private RectTransform _classContent;
        [SerializeField]
        private GameObject _classPrefab;
        [SerializeField]
        private float _classDeltaY = 20f;

        private List<Animator> _itemsOnScene = new List<Animator>();
        private Sprite[] _iconsAtlas;
        private int _openedBlock = -1;
        private int _openedItemIndex = -1;
        private bool _switchTextProcess;
        private bool _switchBlockProcess;

        public void Init(PlayingCharacter _data)
        {
            if (_iconsAtlas == null || _iconsAtlas.Length == 0)
                _iconsAtlas = Resources.LoadAll<Sprite>("Sprites/AllSpells");

            Clear();
            _characterName.text = _data.characterName;
            _race.text = _data.race.raceName;
            _characterLevel.text = _data.playerLevel.generalLevel.ToString();
            ShowLevels();
            Image blockImg = _blocks[3].GetComponent<Image>();
            Text blockTxt = _blocks[3].transform.GetChild(1).GetComponent<Text>();
            if (_data.spells.Count == 0)
            {
                _blocks[3].enabled = false;
                blockImg.color = Color.grey;
                blockTxt.color = Color.grey;
            }
            else
            {
                _blocks[3].enabled = true;
                blockImg.color = Color.white;
                blockTxt.color = Color.white;
            }

            _menuAnim.SetBool("Select", true);
        }

        public void Back()
        {
            if (!_switchBlockProcess && !_switchTextProcess)
            {
                _blocks[_openedBlock].SetBool("Select", false);
                _menuAnim.SetBool("Select", false);
            }
        }

        public void SelectBlock(List<Gameplay.CharacterInfo> _info, List<GameObject> _items, int _newOpened)
        {
            if (_switchBlockProcess || _switchTextProcess)
                return;

            bool clear = false;
            if (_itemsOnScene.Count > 0)
            {
                clear = true;
                _switchTextProcess = true;
                StartCoroutine(SwitchText());
            }

            StartCoroutine(SwitchBlocks(_info, _items, clear));

            if (_openedBlock >= 0)
                _blocks[_openedBlock].SetBool("Select", false);

            _blocks[_newOpened].SetBool("Select", true);
            _openedBlock = _newOpened;

            _switchBlockProcess = true;
        }

        private void ShowLevels()
        {
            if (_classContent.childCount > 0)
            {
                for (int i = 0; i < _classContent.childCount; i++)
                    Destroy(_classContent.GetChild(i).gameObject);
            }

            float sizeY = -1f;
            PlayingCharacter current = GameDataContainer.Instance.currentCharacter;
            for (int i = 0; i < current.playerLevel.currentClasses.Count; i++)
            {
                KeyValuePair<CharacterClass, int> pair = current.playerLevel.currentClasses.ElementAt(i);
                string className = pair.Key.classType.ToString();
                string level = pair.Value.ToString();
                GameObject classGO = Instantiate(_classPrefab);
                RectTransform classRect = classGO.transform as RectTransform;
                if (sizeY < 0f)
                    sizeY = classRect.sizeDelta.y;
                classRect.SetParent(_classContent, false);
                classRect.localPosition = new Vector3(-_classContent.rect.width / 2f, -i * (sizeY + _classDeltaY));
                Text classText = classGO.GetComponent<Text>();
                Text levelText = classRect.GetChild(1).GetComponent<Text>();

                classText.text = className;
                levelText.text = level;
                classRect.sizeDelta = new Vector2(classText.preferredWidth + 10f, classRect.sizeDelta.y);
            }

            _classContent.sizeDelta = new Vector2(_classContent.sizeDelta.x, current.playerLevel.currentClasses.Count * (sizeY + _classDeltaY) - _classDeltaY);
        }

        private void Clear()
        {
            foreach (var item in _itemsOnScene)
                Destroy(item.gameObject);
            _itemsOnScene.Clear();
            _descriptionContent.text = string.Empty;
            _openedBlock = -1;
            _openedItemIndex = -1;
        }

        public void OpenDescription(int _id, string _text)
        {
            if (!_switchBlockProcess && !_switchTextProcess && _openedItemIndex != _id)
            {
                if (_openedItemIndex >= 0)
                    _itemsOnScene[_openedItemIndex].SetBool("Select", false);
                _openedItemIndex = _id;
                _switchTextProcess = true;
                _itemsOnScene[_openedItemIndex].SetBool("Select", true);
                StartCoroutine(SwitchText(_text));
            }
        }

        private IEnumerator SwitchBlocks(List<Gameplay.CharacterInfo> _info, List<GameObject> _items, bool _clear = false)
        {
            if (_clear)
            {
                foreach (var item in _itemsOnScene)
                {
                    item.SetTrigger("Out");
                    Destroy(item.gameObject, 0.25f);
                }

                _itemsOnScene.Clear();
                yield return new WaitForSeconds(0.25f);
            }

            for (int i = 0; i < _items.Count; i++)
            {
                RectTransform itemRect = _items[i].transform as RectTransform;
                Animator itemAnim = _items[i].GetComponent<Animator>();
                Image icon = itemRect.GetChild(1).GetComponent<Image>();
                Text itemName = itemRect.GetChild(2).GetComponent<Text>();
                Sprite iconSprite = _iconsAtlas.Single(s => s.name == _info[i].icon);
                icon.sprite = iconSprite;
                itemName.text = _info[i].infoName;
                int itemId = i;
                _itemsOnScene.Add(itemAnim);

                yield return new WaitForSeconds(0.07f);
            }

            _switchBlockProcess = false;
        }

        private IEnumerator SwitchText(string _newText = "")
        {
            if (!string.IsNullOrEmpty(_descriptionContent.text))
            {
                for (float timer = 0f; timer < 1f; timer += Time.deltaTime / 0.18f)
                {
                    _descriptionContent.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), timer);
                    yield return null;
                }

                if (string.IsNullOrEmpty(_newText))
                {
                    _openedItemIndex = -1;
                    _descriptionContent.text = string.Empty;
                    _descriptionContent.color = Color.white;
                }
            }

            if (!string.IsNullOrEmpty(_newText))
            {
                _descriptionContent.color = new Color(1f, 1f, 1f, 0f);
                string newText = _newText.Replace("<bd>", "\r\n");
                newText = newText.Replace("<br>", "\r\n\r\n");
                _descriptionContent.text = newText;
                _descriptionContent.rectTransform.sizeDelta = new Vector2(_descriptionContent.rectTransform.sizeDelta.x,
                    _descriptionContent.preferredHeight + 20f);

                for (float timer = 0f; timer < 1f; timer += Time.deltaTime / 0.18f)
                {
                    _descriptionContent.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, timer);
                    yield return null;
                }
            }
            else
                _descriptionContent.text = string.Empty;

            _descriptionContent.color = Color.white;
            _switchTextProcess = false;
        }
    }
}