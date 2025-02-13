using UnityEngine;

namespace MathMasters.Entities
{
    public class QuestionDTO
    {
        public string Text;
        public bool IsTextAnswers = true;
        public bool IsQuestionImage = true;
        public int Correct;

        public string[] AnswersText;
        public Sprite[] AnswersImage;
        public Sprite QuestionImage;

        public int Id;
        public bool IsMistakes;



        public QuestionDTO(Question q)
        {
            Text = q.Text;
            IsTextAnswers = q.IsTextAnswers;
            IsQuestionImage = q.IsQuestionImage;
            Correct = q.Correct;

            QuestionImage = q.QuestionImage;
            AnswersText = q.AnswersText != null ? (string[])q.AnswersText.Clone() : null;
            AnswersImage = q.AnswersImage != null ? (Sprite[])q.AnswersImage.Clone() : null;

        }

    }
}
