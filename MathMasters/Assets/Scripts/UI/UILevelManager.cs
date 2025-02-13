using MathMasters.Core;
using UnityEngine;
using UnityEngine.Events;

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
            QuestionManager.OnReadyForContinue +=  _continueButton.ReadyForContinue;
            QuestionManager.OnLevelComplete += (q, s) => _continueButton.ReadyForEnd();
            QuestionManager.OnCorrectAnswer += _progressBar.SetProgress;
            QuestionManager.OnLevelComplete += (q, s) => _progressBar.SetProgress(q.Length);
        }

        private void OnDisable()
        {
            UIQuestion.OnSelected -= _continueButton.ReadyForCheck;
            QuestionManager.OnReadyForContinue -= _continueButton.ReadyForContinue;
            QuestionManager.OnLevelComplete -= (q, s) => _continueButton.ReadyForEnd();
            QuestionManager.OnCorrectAnswer -= _progressBar.SetProgress;
            QuestionManager.OnLevelComplete -= (q, s) => _progressBar.SetProgress(q.Length);
        }
    }
}
