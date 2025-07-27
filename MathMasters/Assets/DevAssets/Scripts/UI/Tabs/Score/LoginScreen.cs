using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathMasters
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _emailInput;

        [SerializeField]
        private TMP_InputField _passwordInput;

        [SerializeField] 
        private Toggle _showPasswordToggle;

        [SerializeField]
        private Button _login;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        public event Action<string, string> OnLoginAttempted;

        private void OnEnable()
        {
            _login.onClick.AddListener(OnLoginButtonClicked);
            _showPasswordToggle.onValueChanged.AddListener(SetPasswordVisible);
        }
        private void OnDisable()
        {
            _login.onClick.RemoveListener(OnLoginButtonClicked);
            _showPasswordToggle.onValueChanged.RemoveListener(SetPasswordVisible);
        }

        public void Show()
        {
            _emailInput.text = string.Empty;
            _passwordInput.text = string.Empty;
            _errorText.text = string.Empty;
            _showPasswordToggle.isOn = false;
            SetPasswordVisible(_showPasswordToggle.isOn);

            gameObject.SetActive(true);
        }

        public void ShowError(string message)
        {
            _errorText.text = message;
        }

        private void OnLoginButtonClicked()
        {
            string email = _emailInput.text.Trim();
            string password = _passwordInput.text.Trim();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowError("Email and password cannot be empty.");
                return;
            }
            OnLoginAttempted?.Invoke(email, password);
        }

        private void SetPasswordVisible(bool isVisible)
        {
            _passwordInput.contentType = isVisible
                ? TMP_InputField.ContentType.Standard 
                : TMP_InputField.ContentType.Password;
         
            _passwordInput.ForceLabelUpdate();
        }
    }
}
