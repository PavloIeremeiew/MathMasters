using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

namespace MathMasters.Entities
{
    [CustomEditor(typeof(Question))]
    public class QuestionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Question question = (Question)target;

            SetText(question);
            SetToggles(question);
            SetImage(question);
            int answersCount = SetAnswersList(question);

            SetCorrectAnswer(question, answersCount);

            Save—hanges();
        }
        private void Save—hanges()
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            EditorUtility.SetDirty(target);
        }

        private void SetText(Question question)
        {
            EditorGUILayout.LabelField("Question Text", EditorStyles.boldLabel);
            question.Text = EditorGUILayout.TextArea(question.Text, GUILayout.Height(120));
        }
        private void SetToggles(Question question)
        {
            question.IsTextAnswers = EditorGUILayout.Toggle("Is Text Answers", question.IsTextAnswers);
            question.IsQuestionImage = EditorGUILayout.Toggle("Is Question with image", question.IsQuestionImage);
        }
        private void SetImage(Question question)
        {
            if (question.IsQuestionImage)
            {
                question.QuestionImage = 
                    (Sprite)EditorGUILayout.ObjectField("Question Image", question.QuestionImage, typeof(Sprite), false);
            }
        }

        private int SetAnswersList(Question question)
        {
            SerializedProperty answersProperty = serializedObject.FindProperty(question.IsTextAnswers ? "answersText" : "answersImage");
            EditorGUILayout.PropertyField(answersProperty, new GUIContent("Answers"), true);

            return answersProperty.arraySize;
        }

        private void SetCorrectAnswer(Question question,int answersCount)
        {
            question.Correct = Mathf.Clamp(question.Correct, 0, Mathf.Max(answersCount - 1, 0));
            question.Correct = EditorGUILayout.IntSlider("Correct Answer", question.Correct, 0, Mathf.Max(answersCount - 1, 0));
        }
    }
}
