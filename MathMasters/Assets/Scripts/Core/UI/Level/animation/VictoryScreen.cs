using MathMasters.Entities;
using TMPro;
using UnityEngine;

namespace MathMasters
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _correct;
        [SerializeField] private TextMeshProUGUI _mistakes;
        public void Show(Question[] questions, ITimer timer)
        {
            _coinCount.text = questions.Length.ToString();
            _time.text = timer.GetElapsedTime();
            int mistakesCount = countMistakes(questions);
            _mistakes.text = mistakesCount.ToString();
            _correct.text = (questions.Length-mistakesCount).ToString();
            _content.SetActive(true);
        }

        private int countMistakes(Question[] questions)
        {
            int count = 0;
            foreach (Question question in questions)
            {
                if (question.IsMistakes)
                    count++;
            }
            return count;
        }
    }
}
