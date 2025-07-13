using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

namespace MathMasters
{
    public class FirebaseAuthService : IAuthService
    {
        private FirebaseAuth _auth;
        private AuthUser _currentUser;

        public AuthUser CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;

        public async Task InitializeAsync()
        {
            var status = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (status != DependencyStatus.Available)
                throw new System.Exception("Firebase dependencies not met: " + status);

            _auth = FirebaseAuth.DefaultInstance;
            UpdateCurrentUser();
        }

        public async Task<bool> TryRegisterOrLoginAsync(string email, string password)
        {
            if (await TryRegisterAsync(email, password))
                return true; 
         
            return await TryLoginAsync(email, password);
        }

        public async Task<bool> TryRegisterAsync(string email, string password)
        {
            try
            {
                await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
                await _auth.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseException e)
            {
                UnityEngine.Debug.LogWarning($"Registration failed: {e.Message}");
                return false;
            }
        }

        public async Task<bool> TryLoginAsync(string email, string password)
        {
            try
            {
                await _auth.SignInWithEmailAndPasswordAsync(email, password);
                UpdateCurrentUser();
                return true;
            }
            catch (FirebaseException e)
            {
                UnityEngine.Debug.LogWarning($"Login failed: {e.Message}");
                return false;
            }
        }

        public void Logout()
        {
            _auth.SignOut();
            _currentUser = null;
        }

        private void UpdateCurrentUser()
        {
            if (_auth?.CurrentUser != null)
            {
                _currentUser = new AuthUser
                {
                    UserId = _auth.CurrentUser.UserId,
                    Email = _auth.CurrentUser.Email
                };
            }
            else
            {
                _currentUser = null;
            }
        }
    }
}
