using MathMasters.Services;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class ScoresTab : Tab
    {
        [Inject] private readonly IAuthService _auth;
        [Inject] private readonly ISaver _saver;
        [Inject] private readonly ILeaderboardLoader _leaderboardLoader;

        [SerializeField]
        private LoginScreen _loginWidget;

        [SerializeField]
        private ScoreScreen _scoreScreen;

        private void OnEnable()
        {
            _loginWidget.OnLoginAttempted += HandleLogin;
        }

        private void OnDisable()
        {
            _loginWidget.OnLoginAttempted -= HandleLogin;
        }

        public override void OnTabSelected()
        {
            if (!_auth.IsLoggedIn)
            {
                _loginWidget.Show();
                _scoreScreen.gameObject.SetActive(false);
            }
            else
            {
                _loginWidget.gameObject.SetActive(false);
                ShowScoreScreen();
            }
        }

        private async void HandleLogin(string email, string password)
        {
            if (await _auth.TryRegisterOrLoginAsync(email, password))
            {
                _loginWidget.gameObject.SetActive(false);
                ShowScoreScreen();
            }
            else
            {
                _loginWidget.ShowError("Login attempt failed. Please check your credentials.");
            }

        }
        
        public async void ShowScoreScreen()
        {
            var coins = await _saver.GetMoney();
            var level = await _saver.GetLevel();
            var list = await _leaderboardLoader.LoadTopPlayersAsync();
            var playerName = _auth.CurrentUser.Name;
            var playerNumber = list.FindIndex(x => x.name == playerName);
            _scoreScreen.Show(coins, level, list, playerNumber);
        }
    }
}
