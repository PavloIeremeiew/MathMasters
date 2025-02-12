using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace MathMasters
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

        public UnityAction Check {  get; set; }
        public UnityAction Continue {  get; set; }
        public UnityAction End {  get; set; }

        private void Start()
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
            ActivateButton(Check, CHECK_TITLE);
        }

        public void ReadyForContinue()
        {
            ActivateButton(Continue, CONTINUE_TITLE);
        }
        public void ReadyForEnd()
        {
            ActivateButton(End, END_TITLE);
        }

        private void ActivateButton(UnityAction sub, string Title)
        {
            _continueButtonText.text = Title;
            _continueButtonImage.sprite = _activeButton;
            _continueButton.interactable = true;
            _continueButton.onClick.AddListener(sub);
            _continueButton.onClick.AddListener(_soundManager.PlayClickSound);
        }

    }
}
