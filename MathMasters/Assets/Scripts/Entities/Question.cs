using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Entities
{
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "ScriptableObjects/Question")]
    public class Question : ScriptableObject
    {
        public string Text;
        public bool IsTextAnswers;

        [SerializeField] private string[] answersText;
        [SerializeField] private Sprite[] answersImage;

        [Range(0, 3)]
        public int Correct;

        public string[] AnswersText => answersText;
        public Sprite[] AnswersImage => answersImage;
    }
}