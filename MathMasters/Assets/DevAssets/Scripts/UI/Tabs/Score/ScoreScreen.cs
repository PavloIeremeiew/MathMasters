using MathMasters.Services;
using System.Collections.Generic;
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

        [SerializeField]
        private Transform _leaderboardContainer;

        [SerializeField]
        private LiderBoardRowWidget _liderBoardRowWidgetPrefab;

        // liderboard

        public void Show(int coins, int level, IReadOnlyList<LeaderboardData> leaderboards, int playerNumber)
        {
            _coinsText.text = coins.ToString();
            _levelsText.text = "Level "+level;
            gameObject.SetActive(true);

            foreach (Transform child in _leaderboardContainer)
                Destroy(child.gameObject);

            for(int i = 0; i < leaderboards.Count; i++)
            {
                var leaderboardData = leaderboards[i];
                var rowWidget = Instantiate(_liderBoardRowWidgetPrefab, _leaderboardContainer);
                rowWidget.SetData(i + 1, leaderboardData.name, leaderboardData.money, i == playerNumber);
            }
        }
    }
}
