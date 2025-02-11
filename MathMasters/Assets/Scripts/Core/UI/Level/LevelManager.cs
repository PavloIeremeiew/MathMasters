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
        [SerializeField] private Question  question;//for test
        [SerializeField] private UIQuestion _uiQuestion;
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private CorrectAnswerAnimation _correctAnswerAnimation;
        [SerializeField] private WrongAnswerAnimation _wrongAnswerAnimation;

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
            // �������� �� �����
            //����������� �� ����������� ��������� �����
        }

        private void EndQuestion()
        {
            _uiQuestion.gameObject.SetActive(false);
            _continueButton.Deactivation();
        }

        private void CorrectCheck()
        {
            // �������� �� �����
            _correctAnswerAnimation.Show(); //������� ���������
            //�������� ������ �������� � ������ + ������� ���

        }
        private void WrongCheck()
        {
            _wrongAnswerAnimation.Show(question);//������� ��������� �������
            //��������� ������� ������� � ����� ������
        }

    }
}
