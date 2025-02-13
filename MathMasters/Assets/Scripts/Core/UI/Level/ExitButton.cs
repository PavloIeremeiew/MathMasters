using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MathMasters
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _exitMenu;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _dismissButton;
        [Inject] private SoundManager _soundManager;

        private void Start() {
            SetUpButtons();
        }
        private void SetUpButtons()
        {
            SetUpButtonsSound();
            _exitButton.onClick.AddListener(OpenExitMenu);
            _confirmButton.onClick.AddListener(Exit);
            _dismissButton.onClick.AddListener(CloseExitMenu);
        }
        private void SetUpButtonsSound()
        {
            _exitButton.onClick.AddListener(_soundManager.PlayClickSound);
            _confirmButton.onClick.AddListener(_soundManager.PlayClickSound);
            _dismissButton.onClick.AddListener(_soundManager.PlayClickSound);
        }

        private void OpenExitMenu()
        {
            _exitMenu.SetActive(true);
            Debug.Log("OpenExitMenu");
        }
        private void CloseExitMenu() 
        {
            _exitMenu.SetActive(false);
        }
        private void Exit()
        {
            Debug.Log("Exit");
        }

    }
}
