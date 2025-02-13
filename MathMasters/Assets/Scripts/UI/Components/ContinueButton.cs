using MathMasters.Services;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace MathMasters.UI
{
    public class ContinueButton: MonoBehaviour
    {
        private const string CHECK_TITLE = "Check";
        private const string CONTINUE_TITLE = "Continue";
        private const string END_TITLE = "Claim";

        [SerializeField]private Button _continueButton;
        private TextMeshProUGUI _continueButtonText;
        private Image _continueButtonImage;

        [SerializeField] private Sprite _activeButton;
        [SerializeField] private Sprite _deactiveButton;

        [Inject] private SoundManager _soundManager;

        public static Action Check;
        public static Action Continue;
        public static Action End;

        private void Awake()
        {
            SetContinueButton();
        }
        private void SetContinueButton()
        {
            _continueButtonText = _continueButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _continueButtonImage = _continueButton.GetComponent<Image>();
        }
        public void Deactivation()
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButtonText.text = string.Empty;
            _continueButtonImage.sprite = _deactiveButton;
            _continueButton.interactable = false;
        }
        public void ReadyForCheck()
        {
            ActivateButton(OnCheck, CHECK_TITLE);
        }
        private void OnCheck()
        {
            OnClick(Check);
        }
        public void ReadyForContinue()
        {
            ActivateButton(OnContinue, CONTINUE_TITLE);
        }
        private void OnContinue()
        {
            OnClick(Continue);
        }
        private void OnClick(Action action)
        {
            Deactivation();
            action?.Invoke();
            
        }
        public void ReadyForEnd()
        {
            ActivateButton(() => End?.Invoke(), END_TITLE);
        }

        private void ActivateButton(UnityAction sub, string Title)
        {
            _continueButtonText.text = Title;
            _continueButtonImage.sprite = _activeButton;
            _continueButton.interactable = true;
            _continueButton.onClick.AddListener(_soundManager.Stop);
            _continueButton.onClick.AddListener(_soundManager.PlayClickSound);
            _continueButton.onClick.AddListener(sub);
        }

    }
}
