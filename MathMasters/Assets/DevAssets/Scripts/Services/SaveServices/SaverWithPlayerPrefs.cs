using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace MathMasters.Services
{
    public class SaverWithPlayerPrefs : ISaver
    {
        private const string MONEY_NAME = "Money";
        private const string LEVEL_NAME = "Level";
        private const string BLOCK_NAME = "Block";
        private const string LAST_UPDATE_KEY = "last_update";

        public UniTask<int> GetBlock()
        {
            return UniTask.FromResult(PlayerPrefs.GetInt(BLOCK_NAME));
        }

        public UniTask<int> GetLevel()
        {
            return UniTask.FromResult(PlayerPrefs.GetInt(LEVEL_NAME));
        }

        public UniTask<int> GetMoney()
        {
            return UniTask.FromResult(PlayerPrefs.GetInt(MONEY_NAME));
        }

        public void SaveBlock(int number)
        {
            PlayerPrefs.SetInt(BLOCK_NAME, number);
        }

        public void SaveLevel(int number)
        {
            PlayerPrefs.SetInt(LEVEL_NAME, number);
        }

        public void SaveMoney(int amount)
        {
            PlayerPrefs.SetInt(MONEY_NAME, amount);
        }

        public void SaveLastUpdate(DateTime time) =>
        PlayerPrefs.SetString(LAST_UPDATE_KEY, time.ToString("o"));
        public DateTime GetLastUpdate() =>
            DateTime.TryParse(PlayerPrefs.GetString(LAST_UPDATE_KEY, ""), out var result)
            ? result : DateTime.MinValue;

        public void SaveAll(int money, int level, int block, DateTime time)
        {
            SaveMoney(money);
            SaveLevel(level);
            SaveBlock(block);
            SaveLastUpdate(time);
            PlayerPrefs.Save();
        }
    }
}
