using DG.Tweening;
using System;
using UnityEngine;

namespace MathMasters
{
    public class TabsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] _tabs;
        [SerializeField] private GameObject _hightlight;
        [SerializeField] private TabButton[] _buttons;

        [SerializeField] private float _moveTime=0.3f;

        private void Start()
        {
            SubAllButtons();
            OpenTab(0);            
        }

        private void SubAllButtons()
        {
            for (int i = 0; i < _buttons.Length; i++) 
            {
                int index = i;
                _buttons[i].Subscribe(() => OpenTab(index));
            }
        }

        private void OpenTab(int index)
        {
            CloseAllTabs();
            ActiveAllButtons();
            SetUpCurrentTab(index);
            MoveHightlight(index);

        }

        private void CloseAllTabs() 
        {
            foreach (GameObject tab in _tabs) {
                tab.SetActive(false);
            }
        }
        private void ActiveAllButtons()
        {
            foreach (TabButton button in _buttons)
            {
                button.Activate();
            }
        }

        private void SetUpCurrentTab(int index)
        {
            _tabs[index].SetActive(true);
            _buttons[index].Deactivate();
        }

        private void MoveHightlight(int index)
        {
            float xPos = _buttons[index].transform.position.x;
            AnimMoveHightlight(xPos);
        }
        private void AnimMoveHightlight(float xPos)
        {
            _hightlight.transform.DOKill();
            _hightlight.transform.DOMoveX(xPos, _moveTime).SetEase(Ease.OutBack);
        }
    }
}
