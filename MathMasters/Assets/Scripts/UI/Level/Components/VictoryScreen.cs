using MathMasters.Animation;
using MathMasters.Entities;
using MathMasters.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace MathMasters.UI
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _correct;
        [SerializeField] private TextMeshProUGUI _mistakes;
        [SerializeField] private VictoryAnimation _victoryAnimation;

        [Inject] private SoundManager _soundManager;
        [Inject] private ISaver _saver;
        [Inject] private ISceneNavigator _sceneNavigator;
        [Inject] private LevelDataSignal _signal;

        private string _timeValue;
        private int _mistakesCount;
        private int _corectCount;
        private int _money;
        private void OnEnable()
        {
            ContinueButton.End += _sceneNavigator.OpenMenu;
        }
        private void OnDisable()
        {
            ContinueButton.End -= _sceneNavigator.OpenMenu;
        }
        public void Show(QuestionDTO[] questions, string timer)
        {
            SetUpValues(questions, timer);
            SetReward();
            Visual();
            _victoryAnimation.Show();
            _soundManager.PlayWinSound();
        }
        private void SetUpValues(QuestionDTO[] questions, string timer)
        {
            _timeValue = timer;
            _mistakesCount = countMistakes(questions);
            _corectCount = questions.Length - _mistakesCount;
            _money = questions.Length;
        }
        private void SetReward()
        {
            if (!_signal.IsReplay)
            {
                LevelUp();
                SetMoney();
            }
            else
            {
                _coinCount.text = "0";
            }
        }

        private void LevelUp()
        {
            int neLevel = 1 + _saver.GetLevel();
            _saver.SaveLevel(neLevel);
        }


        private void Visual()
        {
            SetInfo();
            _content.SetActive(true);
        }
        private void SetMoney()
        {
            _coinCount.text = _money.ToString();
            int newManeyValue = _money + _saver.GetMoney();
            _saver.SaveMoney(newManeyValue);
        }
        private void SetInfo()
        {
            _time.text = _timeValue;
            _mistakes.text = _mistakesCount.ToString();
            _correct.text = _corectCount.ToString();
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
