using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using MathMasters.Services;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class FirebaseSaver : ISaver, IInitializable
    {
        private const string USER_KEY = "users";
        private const string NAME_KEY = "name";
        private const string MONEY_KEY = "money";
        private const string LEVEL_KEY = "level";
        private const string BLOCK_KEY = "block";
        private const string LAST_UPDATE_KEY = "lastUpdate";

        private bool _isInitiallizing;

        [Inject] private readonly IAuthService _authService;

        private readonly SaverWithPlayerPrefs _fallbackSaver = new();
        private DatabaseReference _db;
        private string _userId;
        private string _name;

        private int _cachedMoney;
        private int _cachedLevel;
        private int _cachedBlock;
        private DateTime _lastUpdate = DateTime.MinValue;

        public async void Initialize()
        {
            _isInitiallizing = true;
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus != DependencyStatus.Available)
                throw new FirebaseException();

            _db = FirebaseDatabase.DefaultInstance.RootReference;
            _authService.OnUserChanged += OnUserChanged;
            OnUserChanged(_authService.CurrentUser);
        }

        private void OnUserChanged(AuthUser user)
        {
            _userId = user.UserId;
            _name = user.Email.Split('@').First();
         
            if (string.IsNullOrEmpty(_userId))
            {
                Debug.LogError("FirebaseSaver: No user is currently signed in.");
                return;
            }

            LoadAll();
        }

        private void LoadAll()
        {
            _db.Child(USER_KEY).Child(_userId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogWarning("Firebase load failed, using fallback.");
                    LoadFromFallback();
                    return;
                }

                var snapshot = task.Result;

                var cloudMoney = snapshot.Child(MONEY_KEY).Exists ? int.Parse(snapshot.Child(MONEY_KEY).Value.ToString()) : 0;
                var cloudLevel = snapshot.Child(LEVEL_KEY).Exists ? int.Parse(snapshot.Child(LEVEL_KEY).Value.ToString()) : 0;
                var cloudBlock = snapshot.Child(BLOCK_KEY).Exists ? int.Parse(snapshot.Child(BLOCK_KEY).Value.ToString()) : 0;
                var cloudLastUpdate = snapshot.Child(LAST_UPDATE_KEY).Exists
                    ? DateTime.Parse(snapshot.Child(LAST_UPDATE_KEY).Value.ToString())
                    : DateTime.MinValue;

                if (!snapshot.Child(NAME_KEY).Exists)
                {
                    Save(NAME_KEY, _name);
                }

                var localLastUpdate = _fallbackSaver.GetLastUpdate();

                if (cloudLastUpdate > localLastUpdate)
                {
                    _cachedMoney = cloudMoney;
                    _cachedLevel = cloudLevel;
                    _cachedBlock = cloudBlock;
                    _lastUpdate = cloudLastUpdate;

                    _fallbackSaver.SaveAll(cloudMoney, cloudLevel, cloudBlock, cloudLastUpdate);
                }
                else
                {
                    LoadFromFallback();
                    SaveAllToFirebase();
                }
                _isInitiallizing = false;
            });
        }

        private async void LoadFromFallback()
        {
            _cachedMoney = await _fallbackSaver.GetMoney();
            _cachedLevel = await _fallbackSaver.GetLevel();
            _cachedBlock = await _fallbackSaver.GetBlock();
            _lastUpdate = _fallbackSaver.GetLastUpdate();
        }

        public void SaveMoney(int amount)
        {
            _cachedMoney = amount;
            Save(MONEY_KEY, amount);
            _fallbackSaver.SaveMoney(amount);
        }

        public async UniTask<int> GetMoney()
        {
            while (_isInitiallizing)
            {
                await UniTask.DelayFrame(1);
            }
            return _cachedMoney;
        }

        public void SaveLevel(int number)
        {
            _cachedLevel = number;
            Save(LEVEL_KEY, number);
            _fallbackSaver.SaveLevel(number);
        }

        public async UniTask<int> GetLevel()
        {
            while (_isInitiallizing)
            {
                await UniTask.DelayFrame(1);
            }
            return _cachedLevel;
        }

        public void SaveBlock(int number)
        {
            _cachedBlock = number;
            Save(BLOCK_KEY, number);
            _fallbackSaver.SaveBlock(number);
        }

        public async UniTask<int> GetBlock()
        {
            while (_isInitiallizing)
            {
                await UniTask.DelayFrame(1);
            }
            return _cachedBlock;
        }

        private void Save<T>(string key, T value)
        {
            _lastUpdate = DateTime.UtcNow;
            _db.Child(USER_KEY).Child(_userId).Child(key).SetValueAsync(value);
            _db.Child(USER_KEY).Child(_userId).Child(LAST_UPDATE_KEY).SetValueAsync(_lastUpdate.ToString("o"));
            _fallbackSaver.SaveLastUpdate(_lastUpdate);
        }

        private void SaveAllToFirebase()
        {
            Save(MONEY_KEY, _cachedMoney);
            Save(LEVEL_KEY, _cachedLevel);
            Save(BLOCK_KEY, _cachedBlock);
        }
    }
}
