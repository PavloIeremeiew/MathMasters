using MathMasters.Entities;
using MathMasters.Services;
using MathMasters.UI;
using System;
using UnityEngine;
using Zenject;

namespace MathMasters.Core
{
    public class QuestionManager : MonoBehaviour
    {
        [Inject] private LevelDataSignal _signal;
        [SerializeField] private UIQuestion _uiQuestion;

        private ITimer _timer = new Timer();
        private QuestionDTO[] _qustionArray;
        private QuestionDTO _currentQustion;
        private int _currentQustionIndex = 0;

        public static event Action<QuestionDTO[], string> OnLevelComplete;
        public static event Action<int> OnCorrectAnswer;
        public static event Action OnReadyForContinue;
        public static event Action<QuestionDTO> OnWrongAnswer;

        private void Start()
        {
            _timer.Start();
            SetUpLevel();
        }
        private void OnEnable()
        {
            ContinueButton.Check += Check;
            ContinueButton.Check += CheckToContinue;
            ContinueButton.Check += HideQuestion;
            ContinueButton.Continue += SetQuestion;
        }
        private void OnDisable()
        {
            ContinueButton.Check -= Check;
            ContinueButton.Check -= CheckToContinue;
            ContinueButton.Check -= HideQuestion;
            ContinueButton.Continue -= SetQuestion;
        }

        private void SetUpLevel()
        {
            _qustionArray = _signal.CurrentLevel.SetUpLevel();
            SetQuestion();
        }
        private void SetQuestion()
        {
            _currentQustion = _qustionArray[_currentQustionIndex];
            _uiQuestion.Question = _currentQustion;
            _uiQuestion.gameObject.SetActive(true);
        }
        private void HideQuestion()
        {
            _uiQuestion.gameObject.SetActive(false);
        }

        private void Check()
        {
            if (_uiQuestion.IsCorect)
            {
                CorrectCheck();
            }
            else
            {
                WrongCheck();
            }
        }
        private void CheckToContinue()
        {
            if (_currentQustionIndex < _qustionArray.Length)
            {
                OnReadyForContinue?.Invoke();
            }
            else
            {
                VictoryScreen();

            }
        }

        private void CorrectCheck()
        {
            _currentQustionIndex++;
            VictoryChek();
        }
        private void VictoryChek()
        {
            if (_currentQustionIndex < _qustionArray.Length)
            {
                OnCorrectAnswer?.Invoke(_currentQustionIndex);
            }
        }
        private void WrongCheck()
        {
            _currentQustion.IsMistakes = true;
            OnWrongAnswer?.Invoke(_currentQustion);
            _qustionArray.MoveToEnd(_currentQustionIndex);
        }
        private void VictoryScreen()
        {
            _timer.Stop();
            string time = _timer.GetElapsedTime();
            OnLevelComplete?.Invoke(_qustionArray, time);
        }
    }
}