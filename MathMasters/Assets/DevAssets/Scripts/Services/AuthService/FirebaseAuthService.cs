using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;

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

        public async Task RegisterAsync(string email, string password)
        {
            var result = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            UpdateCurrentUser();
        }

        public async Task LoginAsync(string email, string password)
        {
            var result = await _auth.SignInWithEmailAndPasswordAsync(email, password);
            UpdateCurrentUser();
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
