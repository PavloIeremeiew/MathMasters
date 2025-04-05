using MathMasters.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathMasters
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class QuestionButton : MonoBehaviour
    {
        private Button _button;        
        private Image _frame;
        [SerializeField] private Image _contenImage;
        [SerializeField] private TextMeshProUGUI _contentText;
        [SerializeField] private Sprite _smallFrame;
        [SerializeField] private Sprite _bigFrame;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _highlightColor = Color.cyan;

        private void Awake()
        {
            _button = GetComponent<Button>();  
            _frame = GetComponent<Image>();
        }

        public void HighlightOn()
        {
            _frame.color = _highlightColor;
        }
        public void HighlightOff()
        {
            _frame.color = _defaultColor;
        }
        public void Subscribe(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
        public void SetAnswersImage(Sprite sprite)
        {
            _contenImage.sprite = sprite;
            _contenImage.gameObject.SetActive(true);
            SetBigFrame();
        }
        public void HideImage()
        {
            _contenImage.gameObject.SetActive(false);
            SetSmallFrame();
        }
        public void SetAnswersText(string text)
        {
            _contentText.text = text;
            _contentText.gameObject.SetActive(true);
        }
        public void HideText()
        {
            _contentText.text= string.Empty;
            _contentText.gameObject.SetActive(false);
        }
        private void SetBigFrame()
        {
            _frame.sprite = _bigFrame;
        }
        private void SetSmallFrame()
        {
            _frame.sprite = _smallFrame;
        }
    }
}
