using Dnd.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dnd.Game
{
    public class GameView : MonoBehaviour
    {
        public CharacterSheetView csView;

        [SerializeField]
        private GameObject[] _arrows;
        [SerializeField]
        private Scrollbar _scroll;
        [SerializeField]
        private GameObject _spellsLevelPanel;
        [SerializeField]
        private float _deltaScroll;

        private bool _scrollState;

        public void ViewCharacters()
        {
            _arrows[1].SetActive(true);
        }

        public void Back()
        {
            csView.Back();
        }

        public void SlideCharacters(float _value)
        {
            if (!_scroll.gameObject.activeSelf)
                return;

            if (_value >= 0.99f && _arrows[0].activeSelf)
                _arrows[0].SetActive(false);
            else if (_value < 0.99f && !_arrows[0].activeSelf)
                _arrows[0].SetActive(true);

            if (_value < 0.01f && _arrows[1].activeSelf)
                _arrows[1].SetActive(false);
            else if (_value >= 0.01f && !_arrows[1].activeSelf)
                _arrows[1].SetActive(true);
        }

        public void SelectBlock(List<Gameplay.CharacterInfo> _info, List<GameObject> _items, int _index)
        {
            if (_index != 3 && _spellsLevelPanel.activeSelf)
                _spellsLevelPanel.SetActive(false);

            csView.SelectBlock(_info, _items, _index);
        }

        public void TapScroll(int _direction)
        {
            if (!_scrollState)
            {
                _scrollState = true;
                StartCoroutine(SmoothScroll(_direction));
            }
        }

        public void SelectCharacter(PlayingCharacter _character)
        {
            csView.Init(_character);
        }

        private IEnumerator SmoothScroll(int _direction)
        {
            float startValue = _scroll.value;
            float endValue = Mathf.Clamp01(startValue + _direction * _deltaScroll);

            for (float timer = 0f; timer < 1f; timer += Time.deltaTime / 0.12f)
            {
                float t = 0.5f * (Mathf.Sin((timer - 0.5f) * Mathf.PI) + 1f);
                _scroll.value = Mathf.Lerp(startValue, endValue, t);
                yield return null;
            }

            _scroll.value = endValue;
            _scrollState = false;
        }
    }
}