using UnityEngine;

namespace MathMasters.Entities
{
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "ScriptableObjects/Question")]
    public class Question : ScriptableObject
    {
        public string Text;
        public bool IsTextAnswers = true;
        public bool IsQuestionImage = true;
        [Range(0, 3)] public int Correct;

        [SerializeField, HideInInspector] private string[] answersText;
        [SerializeField, HideInInspector] private Sprite[] answersImage;
        [SerializeField, HideInInspector] private Sprite questionImage;
        
        public Sprite QuestionImage
        {
            get => questionImage;
            set => questionImage = value;
        }
        public string[] AnswersText
        {
            get => answersText;
            set => answersText = value;
        }
        public Sprite[] AnswersImage
        {
            get => answersImage;
            set => answersImage = value;
        }
    }
}