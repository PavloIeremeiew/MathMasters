using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters
{
    public interface ISaver 
    {
        public void SaveMoney(int amount);
        public int GetMoney();

        public void SaveLevel(int number);
        public int GetLevel();

        public void SaveBlock(int number);
        public int GetBlock();
    }
}
