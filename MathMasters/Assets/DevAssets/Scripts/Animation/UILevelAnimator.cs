using MathMasters.Core;
using MathMasters.Services;
using MathMasters.UI;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace MathMasters.Animation
{
    public class UILevelAnimator : MonoBehaviour
    {
        [SerializeField] private CorrectAnswerAnimation _correctAnswerAnimation;
        [SerializeField] private WrongAnswerAnimation _wrongAnswerAnimation;
        [SerializeField] private VictoryScreen _victoryScreen;
        [Inject] private SoundManager _soundManager;

        private void OnEnable()
        {
            QuestionManager.OnLevelComplete += _victoryScreen.Show;
            QuestionManager.OnCorrectAnswer += Correct;
            QuestionManager.OnWrongAnswer += _wrongAnswerAnimation.Show;
            ContinueButton.Continue += Hide;
        }
        private void OnDisable()
        {
            QuestionManager.OnLevelComplete -= _victoryScreen.Show;
            QuestionManager.OnCorrectAnswer -= Correct;
            QuestionManager.OnWrongAnswer -= _wrongAnswerAnimation.Show;
            ContinueButton.Continue -= Hide;
        }
        private void Correct(int i)
        {
            _correctAnswerAnimation.Show();
        }
        private void Hide()
        {
            _correctAnswerAnimation.Hide();
            _wrongAnswerAnimation.Hide();
        }
    }
}
