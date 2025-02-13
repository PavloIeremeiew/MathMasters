using MathMasters.Entities;
using TMPro;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _correct;
        [SerializeField] private TextMeshProUGUI _mistakes;

        [Inject] private SoundManager _soundManager;
        [Inject] private ISaver _saver;

        public void Show(QuestionDTO[] questions, ITimer timer)
        {
            SetMoney(questions.Length);
            Visual(questions, timer);
            _soundManager.PlayWinSound();
        }

        private void Visual(QuestionDTO[] questions, ITimer timer)
        {
            string time = timer.GetElapsedTime();
            int mistakesCount = countMistakes(questions);
            SetInfo(mistakesCount, questions.Length, time);
            _content.SetActive(true);
        }
        private void SetMoney(int count)
        {
            _coinCount.text = count.ToString();
            int newManeyValue = count + _saver.GetMoney();
            _saver.SaveMoney(newManeyValue);
            Debug.Log(newManeyValue);
        }
        private void SetInfo(int mistakesCount,int count, string time)
        {
            _time.text = time;
            _mistakes.text = mistakesCount.ToString();
            _correct.text = (count - mistakesCount).ToString();
        }


        private int countMistakes(QuestionDTO[] questions)
        {
            int count = 0;
            foreach (QuestionDTO question in questions)
            {
                if (question.IsMistakes)
                    count++;
            }
            return count;
        }
    }
}
