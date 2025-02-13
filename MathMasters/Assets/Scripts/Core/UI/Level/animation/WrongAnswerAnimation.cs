using MathMasters.Entities;
using MathMasters.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MathMasters
{
    public class WrongAnswerAnimation : MonoBehaviour
    {
        private const string ANSWER_TEXT = "Correct answer: ";

        private readonly Vector2 CONTEXT_IMAGE_MAX_SIZE = new Vector2(520, 360);
        private readonly Vector2 ANSWER_IMAGE_MAX_SIZE = new Vector2(360, 360);

        [SerializeField] private GameObject _content;
        [SerializeField] private RectTransform _layoutRoot;
        [SerializeField] private TextMeshProUGUI _context;
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private Image _contextImage;
        [SerializeField] private Image _answerImage;

        [Inject] private SoundManager _soundManager;

        public void Hide()
        {
            _content.SetActive(false);

            _contextImage.gameObject.SetActive(false);
            _answerImage.gameObject.SetActive(false);
            _context.text = string.Empty;
            _answerText.text = ANSWER_TEXT;
        }

        public void Show(QuestionDTO question)
        {
            _content.SetActive(true);
            SetUpContext(question);
            SetUpAnswer(question);
            _soundManager.PlayWrongSound();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutRoot);
        }
        private void SetUpContext(QuestionDTO question)
        {
            if (question.IsQuestionImage)
            {
                _contextImage.ResizeImage(question.QuestionImage, CONTEXT_IMAGE_MAX_SIZE);
                _contextImage.gameObject.SetActive(true);
            }
            _context.text = question.Text;
            
        }

        private void SetUpAnswer(QuestionDTO question)
        {
            int index = question.Correct;
            if (question.IsTextAnswers)
            {
                _answerText.text += question.AnswersText[index];
            }
            else
            {
                _answerImage.ResizeImage(
                    question.AnswersImage[index], ANSWER_IMAGE_MAX_SIZE);
                _answerImage.gameObject.SetActive(true);
            }
        }
    }
}
