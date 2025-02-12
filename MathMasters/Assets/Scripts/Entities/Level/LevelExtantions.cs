using MathMasters.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Entities
{
    public static class LevelExtantions 
    {
        public static void SetUpLevel(this Question[] level)
        {
            for (int i = 0; i < level.Length; i++)
            {
                level[i].Id = i + 1;
                level[i].IsMistakes = false;
            }
        }
        public static void MoveToEnd(this Question[] array, int index)
        {
            if (array == null || array.Length == 0 || index < 0 || index >= array.Length) return;

            Question item = array[index];

            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[array.Length - 1] = item;
        }
    }
}
