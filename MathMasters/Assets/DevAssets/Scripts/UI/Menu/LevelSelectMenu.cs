using MathMasters.Entities;
using MathMasters.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MathMasters
{
    public class LevelSelectMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _block;
        [SerializeField] private TextMeshProUGUI _step;
        [SerializeField] private TextMeshProUGUI _goal;
        [SerializeField] private TextMeshProUGUI _reward;
        [SerializeField] private Button _dismissButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private GameObject _menu;

        [Inject] private SoundManager _soundManager;
        [Inject] private ISceneNavigator _navigator;

        private void Start()
        {
            _confirmButton.onClick.AddListener(_soundManager.PlayClickSound);
            _dismissButton.onClick.AddListener(_soundManager.PlayClickSound);
            _dismissButton.onClick.AddListener(Hide);
            _confirmButton.onClick.AddListener(Open);
        }

        public void Show(LevelInfo info, int goal, int reward)
        {
            _block.text = $"BLOCK {info.Block+1}.";
            _step.text = $"STEP {info.Level+1}";
            _goal.text = $"{goal} tasks";
            _reward.text = reward.ToString();
            _menu.SetActive(true);
        }
        private void Hide()
        {
            _menu.SetActive(false);
        }
        private void Open()
        {
            _navigator.OpenLevel();
        }
    }
}
