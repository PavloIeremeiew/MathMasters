using MathMasters.Entities;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathMasters
{
    public class UIQuestion : MonoBehaviour
    {
        [SerializeField] private Question _question;

        [SerializeField] private RectTransform _layoutGroupRoot;
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI[] _answersTexts;
        [SerializeField] private Image[] _answersImages;
        [SerializeField] private Button[] _answers;


        private void Start()
        {
           Show();
        }

        private void Show()
        {
            SetText();
            SetImage();
            SetAnswers();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroupRoot);
        }

        private void SetText()
        {
            _numberText.text = $"Task{_question.Id}";
            _text.text = _question.Text;
        }
        private void SetImage()
        {
            if (_question.IsQuestionImage)
            {
                _image.sprite = _question.QuestionImage;
                _image.SetNativeSize();
                _image.gameObject.SetActive(true);
            }
        }
        private void SetAnswers()
        {
            if (_question.IsTextAnswers)
               SetAnswersTexts();

            else
                SetAnswersImages();

        }
        private void SetAnswersTexts()
        {
            for (int i = 0; i < _question.AnswersText.Length; i++)
            {
                _answersTexts[i].text = _question.AnswersText[i];
                _answersTexts[i].gameObject.SetActive(true);
                
            }
            
        }
        private void SetAnswersImages()
        {
            for (int i = 0; i < _question.AnswersImage.Length; i++)
            {
                _answersImages[i].sprite = _question.AnswersImage[i];
                _answersImages[i].gameObject.SetActive(true);
                
            }
        }

        private void Clear()
        {
            _numberText.text = string.Empty;
            _text.text = string.Empty;
            _image.gameObject.SetActive(false);

            foreach (TextMeshProUGUI tx in _answersTexts)
            {
                tx.gameObject.SetActive(false);
            }
            foreach (Image im in _answersImages)
            {
                im.gameObject.SetActive(false);
            }
        }
    }
}
