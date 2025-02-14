using MathMasters.Entities;
using MathMasters.Services;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MathMasters.UI
{
    public class UIQuestion : MonoBehaviour
    {
        private readonly Vector2 IMAGE_MAX_SIZE = new Vector2(520, 360);
        public QuestionDTO Question { private get; set; }

        [SerializeField] private RectTransform _layoutGroupRoot;
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI[] _answersTexts;
        [SerializeField] private Image[] _answersImages;
        [SerializeField] private Button[] _answers;

        [Inject] private SoundManager _soundManager;
        private int selectedAnswerIndex = -1;

        public static event Action OnSelected;

        public bool IsCorect =>
             selectedAnswerIndex == Question.Correct;

        private void OnEnable()
        {
            StartCoroutine(Show());
        }
        private void OnDisable()
        {
            Clear();
        }
        private void Start()
        {
            for (int i = 0; i < _answers.Length; i++)
            {
                int index = i;
                _answers[i].onClick.AddListener(() => SelectAnswer(index));
            }
        }
        private IEnumerator Show()
        {
            SetText();
            SetImage();
            SetAnswers();
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroupRoot);
        }

        private void SetText()
        {
            _numberText.text = $"Task{Question.Id}";
            _text.text = Question.Text;
        }
        private void SetImage()
        {
            if (Question.IsQuestionImage)
            {
                _image.ResizeImage(Question.QuestionImage, IMAGE_MAX_SIZE);
                _image.gameObject.SetActive(true);
            }
        }
        private void SetAnswers()
        {
            if (Question.IsTextAnswers)
                SetAnswersTexts();

            else
                SetAnswersImages();

        }
        private void SetAnswersTexts()
        {
            for (int i = 0; i < Question.AnswersText.Length; i++)
            {
                _answersTexts[i].text = Question.AnswersText[i];
                _answersTexts[i].gameObject.SetActive(true);
            }
        }
        private void SetAnswersImages()
        {
            for (int i = 0; i < Question.AnswersImage.Length; i++)
            {
                _answersImages[i].sprite = Question.AnswersImage[i];
                _answersImages[i].gameObject.SetActive(true);

            }
        }

        private void Clear()
        {
            selectedAnswerIndex = -1;
            ClearCondition();
            ClearButtons();
        }
        private void ClearCondition()
        {
            _numberText.text = string.Empty;
            _text.text = string.Empty;
            _image.gameObject.SetActive(false);
        }

        private void ClearButtons()
        {
            ClearButtonsText();
            ClearButtonsImages();
            HighlightButton(selectedAnswerIndex);
        }
        private void ClearButtonsText()
        {
            foreach (TextMeshProUGUI tx in _answersTexts)
            {
                tx.gameObject.SetActive(false);
            }
        }
        private void ClearButtonsImages()
        {
            foreach (Image im in _answersImages)
            {
                im.gameObject.SetActive(false);
            }
        }

        private void SelectAnswer(int index)
        {
            CheckOnSelect();
            SetButtoneffcts(index);
            selectedAnswerIndex = index;
        }
        private void CheckOnSelect()
        {
            if (selectedAnswerIndex == -1)
            {
                OnSelected?.Invoke();
            }
        }

        private void SetButtoneffcts(int index)
        {
            _soundManager.PlaySelectSound();
            HighlightButton(index);
        }

        private void HighlightButton(int index)
        {
            for (int i = 0; i < _answers.Length; i++)
            {
                Color color = (i == index) ? Color.cyan : Color.white;
                _answers[i].GetComponent<Image>().color = color;
            }
        }
    }
}
