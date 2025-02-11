using MathMasters.Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathMasters
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Question question;//for test
        [SerializeField] private UIQuestion _uiQuestion;
        [SerializeField] private ContinueButton _continueButton;

        private void Start()
        {
            SetQuestion();
            _continueButton.Check = Check;
            _continueButton.Deactivation();
        }
        private void OnEnable()
        {
            _uiQuestion.OnSelected += _continueButton.ReadyForCheck;
        }
        private void OnDisable()
        {
            _uiQuestion.OnSelected -= _continueButton.ReadyForCheck;
        }
        private void SetQuestion()
        {
            _uiQuestion.Question = question;
            _uiQuestion.gameObject.SetActive(true);
        }
        private void Check()
        {
            _uiQuestion.gameObject.SetActive(false);
            _continueButton.Deactivation();
            if (_uiQuestion.IsCorect)
            {
                CorrectCheck();
            }
            else
            {
                WrongCheck();
            }
            // перевірка чи кінець
            //пвдписатися на продовження наступний квест
        }
        private void CorrectCheck()
        {
            // перевірка чи кінець
            //вивести привітання
            //збільшити індекс завдання в списку + прогрес бар

        }
        private void WrongCheck()
        {
            //вивести правильну відповідь
            //пермістити поточне питання в кінець списку
        }

    }
}
