using MathMasters.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

namespace MathMasters
{
    public class LevelManager : MonoBehaviour // треба почистити код
    {
        [SerializeField] private Level _step;
        [SerializeField] private UIQuestion _uiQuestion;
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private CorrectAnswerAnimation _correctAnswerAnimation;
        [SerializeField] private WrongAnswerAnimation _wrongAnswerAnimation;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private ProgressBar _progressBar;


        private ITimer _timer = new Timer();
        private Question[] _qustionArray;
        private Question _currentQustion;
        private int _currentQustionIndex=0;

        public event Action OnReadyForContinue;
        public event Action OnReadyForEnd;

        private void Start()
        {
            _progressBar.Init();
            _timer.Start();
            SetUpLevel();
            SetUpContinueButton();
        }
        private void OnEnable()
        {
            _uiQuestion.OnSelected += _continueButton.ReadyForCheck;
            OnReadyForContinue += _continueButton.ReadyForContinue;
            OnReadyForEnd += _continueButton.ReadyForEnd;
        }
        private void OnDisable()
        {
            _uiQuestion.OnSelected -= _continueButton.ReadyForCheck;
            OnReadyForContinue -= _continueButton.ReadyForContinue;
            OnReadyForEnd -= _continueButton.ReadyForEnd;
        }

        private void SetUpLevel()
        {
            _qustionArray = _step.Questions.Select(q => new Question
            {
                Text = q.Text,
                IsTextAnswers = q.IsTextAnswers,
                IsQuestionImage = q.IsQuestionImage,
                QuestionImage = q.QuestionImage,
                AnswersText = q.AnswersText != null ? (string[])q.AnswersText.Clone() : null,
                AnswersImage = q.AnswersImage != null ? (Sprite[])q.AnswersImage.Clone() : null,
                Correct = q.Correct
            }).ToArray();
            _qustionArray.SetUpLevel();
           SetQuestion();
        }       

        private void SetQuestion()
        {
            _currentQustion = _qustionArray[_currentQustionIndex];
            _uiQuestion.Question = _currentQustion;
            _uiQuestion.gameObject.SetActive(true);
            _continueButton.Deactivation();
        }
        
        private void SetUpContinueButton()
        {
            _continueButton.Check = Check;
            _continueButton.Continue = Continue;
            _continueButton.End = End;
        }

        

        private void Check()
        {
            bool isCorect = _uiQuestion.IsCorect;
            EndQuestion();
            if (isCorect)
            {
                CorrectCheck();
            }
            else
            {
                WrongCheck();
            }

            if (_currentQustionIndex < _qustionArray.Length) 
            {
                OnReadyForContinue?.Invoke();
            } 
        }
        private void Continue()
        {            
            _correctAnswerAnimation.Hide();
            _wrongAnswerAnimation.Hide();
            SetQuestion();
        }
        private void End()
        {
            //повернутися в меню
            Debug.Log("End");
        }

        private void EndQuestion()
        {
            _uiQuestion.gameObject.SetActive(false);
            _continueButton.Deactivation();
        }

        private void CorrectCheck()
        {
            _currentQustionIndex++;
            _progressBar.SetProgress(_currentQustionIndex);
            if (_currentQustionIndex >= _qustionArray.Length) 
            {
                VictoryScreen();
            }
            else
            {
                _correctAnswerAnimation.Show(); 
            }
        }
        private void WrongCheck()
        {
            _currentQustion.IsMistakes = true;
            _wrongAnswerAnimation.Show(_currentQustion);
            _qustionArray.MoveToEnd(_currentQustionIndex);
        }
        private void VictoryScreen()
        {
            _timer.Stop();
            OnReadyForEnd?.Invoke();
            _victoryScreen.Show(_qustionArray, _timer);
            //зберегти нагороду
        }

    }
}
