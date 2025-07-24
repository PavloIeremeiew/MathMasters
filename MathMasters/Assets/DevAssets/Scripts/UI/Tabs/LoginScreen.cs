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
        private Button _login;

        public event Action<string, string> OnLoginAttempted;

        private void OnEnable()
        {
            _login.onClick.AddListener(OnLoginButtonClicked);
        }
        private void OnDisable()
        {
            _login.onClick.RemoveListener(OnLoginButtonClicked);
        }

        private void OnLoginButtonClicked()
        {
            string email = _emailInput.text.Trim();
            string password = _passwordInput.text.Trim();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Debug.LogWarning("Email and password cannot be empty.");
                return;
            }
            OnLoginAttempted?.Invoke(email, password);
        }
    }
}
