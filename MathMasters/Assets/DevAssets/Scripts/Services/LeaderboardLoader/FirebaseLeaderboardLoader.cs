using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class FirebaseLeaderboardLoader : ILeaderboardLoader, IInitializable
    {
        private DatabaseReference _db;

        public async void Initialize()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus != DependencyStatus.Available)
                throw new FirebaseException();

            _db = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public async UniTask<List<LeaderboardData>> LoadTopPlayersAsync(int topCount = 10)
        {
            var tcs = new TaskCompletionSource<DataSnapshot>();

            await _db.Child(DBConstants.USER_KEY)
                .OrderByChild(DBConstants.MONEY_KEY)
                .LimitToLast(topCount)
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        tcs.SetException(task.Exception ?? new Exception("Failed to load leaderboard"));
                    }
                    else
                    {
                        tcs.SetResult(task.Result);
                    }
                });

            var snapshot = await tcs.Task;
            var result = new List<LeaderboardData>();

            foreach (var child in snapshot.Children)
            {
                var json = child.GetRawJsonValue();
                var player = JsonUtility.FromJson<LeaderboardData>(json);
                result.Add(player);
            }

            result.Reverse();
            return result;
        }
    }

    public interface ILeaderboardLoader
    {
        UniTask<List<LeaderboardData>> LoadTopPlayersAsync(int topCount = 10);
    }
}