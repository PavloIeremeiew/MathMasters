using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathMasters
{
    public class ContinueButton: MonoBehaviour
    {
        private const string CHECK_TITLE = "Check";

        [SerializeField]private Button _continueButton;
        private TextMeshProUGUI _continueButtonText;
        private Image _continueButtonImage;

        [SerializeField] private Sprite _activeButton;
        [SerializeField] private Sprite _deactiveButton;

        public UnityAction Check {  get; set; }
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
            _continueButtonText.text = CHECK_TITLE;
            _continueButtonImage.sprite = _activeButton;
            _continueButton.interactable = true;
            _continueButton.onClick.AddListener(Check);
        }
    }
}
