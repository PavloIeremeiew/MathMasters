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
            // �������� �� �����
            //����������� �� ����������� ��������� �����
        }
        private void CorrectCheck()
        {
            // �������� �� �����
            //������� ���������
            //�������� ������ �������� � ������ + ������� ���

        }
        private void WrongCheck()
        {
            //������� ��������� �������
            //��������� ������� ������� � ����� ������
        }

    }
}
