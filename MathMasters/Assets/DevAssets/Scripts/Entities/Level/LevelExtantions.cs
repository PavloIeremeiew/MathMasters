using MathMasters.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace MathMasters.Entities
{
    public static class LevelExtantions 
    {
        public static QuestionDTO[] SetUpLevel(this Level level)
        {
            QuestionDTO[] questions = CloneFromScriptableObject(level);
            SetUpDTOs(questions);
            return questions;
        }

        private static QuestionDTO[] CloneFromScriptableObject(Level level)
        {
            return level.Questions.Select(q => new QuestionDTO(q)).ToArray();
        }

        private static void SetUpDTOs(QuestionDTO[] questions)
        {
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].Id = i + 1;
                questions[i].IsMistakes = false;
            }
        }

        public static void MoveToEnd(this QuestionDTO[] array, int index)
        {
            if (array == null || array.Length == 0 || index < 0 || index >= array.Length) return;

            QuestionDTO item = array[index];

            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[array.Length - 1] = item;
        }
    }
}
