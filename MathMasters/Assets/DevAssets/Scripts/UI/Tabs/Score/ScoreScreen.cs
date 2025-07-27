using MathMasters.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class ScoreScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinsText;

        [SerializeField]
        private TextMeshProUGUI _levelsText;

        // liderboard

        public void Show(int coins, int level)
        {
            _coinsText.text = coins.ToString();
            _levelsText.text = "Level "+level;
            gameObject.SetActive(true);
        }
    }
}
