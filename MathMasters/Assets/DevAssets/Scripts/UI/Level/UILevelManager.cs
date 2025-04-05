using MathMasters.Core;
using MathMasters.Entities;
using UnityEngine;

namespace MathMasters.UI
{
    public class UILevelManager : MonoBehaviour
    {
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private ProgressBar _progressBar;

        public void Start()
        {
            _progressBar.Init();
            _continueButton.Deactivation();
        }
        private void OnEnable()
        {
            UIQuestion.OnSelected += _continueButton.ReadyForCheck;
            QuestionManager.OnReadyForContinue += _continueButton.ReadyForContinue;
            QuestionManager.OnLevelComplete += ReadyForEnd;
            QuestionManager.OnCorrectAnswer += _progressBar.SetProgress;
            QuestionManager.OnLevelComplete += SetProgress;
        }

        private void OnDisable()
        {
            UIQuestion.OnSelected -= _continueButton.ReadyForCheck;
            QuestionManager.OnReadyForContinue -= _continueButton.ReadyForContinue;
            QuestionManager.OnLevelComplete -= ReadyForEnd;
            QuestionManager.OnCorrectAnswer -= _progressBar.SetProgress;
            QuestionManager.OnLevelComplete -= SetProgress;
        }
        private void ReadyForEnd(QuestionDTO[] q, string s)
        {
            _continueButton.ReadyForEnd();
        }
        private void SetProgress(QuestionDTO[] q, string s)
        {
            _progressBar.SetProgress(q.Length);
        }
    }
}
