using MathMasters.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathMasters
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level _step;
        [SerializeField] private UIQuestion _uiQuestion;
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private CorrectAnswerAnimation _correctAnswerAnimation;
        [SerializeField] private WrongAnswerAnimation _wrongAnswerAnimation;

        private Question[] _qustionArray;
        private Question _currentQustion;
        private int _currentQustionIndex=0;

        public event Action OnReadyForContinue;

        private void Start()
        {
            SetUpLevel();
            SetUpContinueButton();
        }
        private void OnEnable()
        {
            _uiQuestion.OnSelected += _continueButton.ReadyForCheck;
            OnReadyForContinue += _continueButton.ReadyForContinue;
        }
        private void OnDisable()
        {
            _uiQuestion.OnSelected -= _continueButton.ReadyForCheck;
            OnReadyForContinue -= _continueButton.ReadyForContinue;
        }

        private void SetUpLevel()
        {
            _qustionArray=(Question[])_step.Questions.Clone();
            _qustionArray.SetQuestionId();
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
            // перевірка чи кінець
            OnReadyForContinue?.Invoke(); //пвдписатися на продовження наступний квест
        }
        private void Continue()
        {            
            _correctAnswerAnimation.Hide();
            _wrongAnswerAnimation.Hide();
            SetQuestion();
        }

        private void EndQuestion()
        {
            _uiQuestion.gameObject.SetActive(false);
            _continueButton.Deactivation();
        }

        private void CorrectCheck()
        {
            _currentQustionIndex++;//збільшити індекс завдання в списку
            //прогрес бар
            if (_currentQustionIndex >= _qustionArray.Length) // перевірка чи кінець
            {
                Debug.Log("Win");
            }
            else
            {
                _correctAnswerAnimation.Show(); //вивести привітання
            }

            
            
            
        }
        private void WrongCheck()
        {
            _wrongAnswerAnimation.Show(_currentQustion);//вивести правильну відповідь
            _qustionArray.MoveToEnd(_currentQustionIndex);//пермістити поточне питання в кінець списку
        }

    }
}
