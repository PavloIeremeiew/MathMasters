using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathMasters
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour
    {
        private Button _button;
        private Image _frame;
        [SerializeField] private Sprite _defaultFrame;
        [SerializeField] private Sprite _highlightFrame;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _frame = GetComponent<Image>();
        }

        public void Subscribe(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void Deactivate()
        {
            HighlightOn();
            _button.interactable = false;
        }
        public void Activate()
        {
            HighlightOff();
            _button.interactable = true;
        }
        private void HighlightOn()
        {
            _frame.sprite = _highlightFrame;
        }
        private void HighlightOff()
        {
            _frame.sprite = _defaultFrame;
        }
    }
}
