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

            question.Text = EditorGUILayout.TextField("Question Text", question.Text);
            question.IsTextAnswers = EditorGUILayout.Toggle("Is Text Answers", question.IsTextAnswers);

            SerializedProperty answersProperty = serializedObject.FindProperty(question.IsTextAnswers ? "answersText" : "answersImage");
            EditorGUILayout.PropertyField(answersProperty, new GUIContent("Answers"), true);

            int answersCount = answersProperty.arraySize;
            question.Correct = Mathf.Clamp(question.Correct, 0, Mathf.Max(answersCount - 1, 0));

            question.Correct = EditorGUILayout.IntSlider("Correct Answer", question.Correct, 0, Mathf.Max(answersCount - 1, 0));

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(question);
        }
    }
}
