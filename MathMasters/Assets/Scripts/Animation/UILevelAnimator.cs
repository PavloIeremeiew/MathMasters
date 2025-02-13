using MathMasters.Core;
using MathMasters.UI;
using UnityEngine;

namespace MathMasters.Animation
{
    public class UILevelAnimator : MonoBehaviour
    {
        [SerializeField] private CorrectAnswerAnimation _correctAnswerAnimation;
        [SerializeField] private WrongAnswerAnimation _wrongAnswerAnimation;
        [SerializeField] private VictoryScreen _victoryScreen;

        private void OnEnable()
        {
            QuestionManager.OnLevelComplete += _victoryScreen.Show;
            QuestionManager.OnCorrectAnswer += (i) => _correctAnswerAnimation.Show();
            QuestionManager.OnWrongAnswer += _wrongAnswerAnimation.Show;
            ContinueButton.Continue += Hide;
        }
        private void OnDisable()
        {
            QuestionManager.OnLevelComplete -= _victoryScreen.Show;
            QuestionManager.OnCorrectAnswer -= (i) => _correctAnswerAnimation.Show();
            QuestionManager.OnWrongAnswer -= _wrongAnswerAnimation.Show;
            ContinueButton.Continue -= Hide;
        }
        private void Hide()
        {
            _correctAnswerAnimation.Hide();
            _wrongAnswerAnimation.Hide();
        }
    }
}
