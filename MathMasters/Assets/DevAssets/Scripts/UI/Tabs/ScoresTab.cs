using Zenject;

namespace MathMasters
{
    public class ScoresTab : Tab
    {
        private IAuthService _auth;

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
                // show login/register UI
            }
            else
            {
                // show scores UI
            }
        }
    }
}
