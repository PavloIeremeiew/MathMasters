using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class ScoresTab : Tab
    {
        private IAuthService _auth;

        [SerializeField]
        private LoginScreen _loginWidget;

        [Inject]
        public async void Init(IAuthService auth)
        {
            _auth = auth;
            await _auth.InitializeAsync();
        }

        public override void OnTabSelected()
        {
            if (!_auth.IsLoggedIn)
            {
                _loginWidget.gameObject.SetActive(true);
            }
            else
            {
                // show scores UI
            }
        }
    }
}
