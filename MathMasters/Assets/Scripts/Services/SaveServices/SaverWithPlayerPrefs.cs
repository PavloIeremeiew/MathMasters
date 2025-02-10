using UnityEngine;

namespace MathMasters
{
    public class SaverWithPlayerPrefs : ISaver
    {
        private const string MONEY_NAME = "Money";
        private const string LEVEL_NAME = "Level";
        private const string BLOCK_NAME = "Block";

        public int GetBlock()
        {
            return PlayerPrefs.GetInt(BLOCK_NAME);
        }

        public int GetLevel()
        {
            return PlayerPrefs.GetInt(LEVEL_NAME);
        }

        public int GetMoney()
        {
            return PlayerPrefs.GetInt(MONEY_NAME);
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
    }
}
