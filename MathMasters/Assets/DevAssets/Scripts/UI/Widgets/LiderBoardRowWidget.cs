using TMPro;
using UnityEngine;

namespace MathMasters
{
    public class LiderBoardRowWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        public void SetData(int position, string name, int score, bool isPlayer)
        {
            _positionText.text = position.ToString();
            _nameText.text = name;
            _scoreText.text = score.ToString();
            if (isPlayer)
            {
                _positionText.color = Color.blue;
                _nameText.color = Color.blue;
                _scoreText.color = Color.blue;
            }
        }

    }
}
