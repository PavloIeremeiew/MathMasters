using MathMasters.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace MathMasters
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _lock;
        [SerializeField] private Sprite _done;
        [SerializeField] private Sprite _active;
        private Image _image;

        private void Awake()
        {
            _image = _button.GetComponent<Image>();
        }

        public void Active(UnityAction action)
        {
            setActive(action, _active);
        }

        public void Lock()
        {
            _button.onClick.RemoveAllListeners();
            _image.sprite = _lock;
            _button.interactable = false;
        }
        public void Done(UnityAction action)
        {
            setActive(action, _done);
        }
        private void setActive(UnityAction action, Sprite sprite)
        {
            _button.onClick.AddListener(action);
            _image.sprite = sprite;
            _button.interactable = true;
        }

    }
}
