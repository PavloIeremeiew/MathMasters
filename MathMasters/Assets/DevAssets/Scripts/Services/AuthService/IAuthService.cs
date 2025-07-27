using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;

namespace MathMasters
{
    public interface IAuthService
    {
        AuthUser CurrentUser { get; }
        bool IsLoggedIn { get; }

        UniTask<bool> TryRegisterOrLoginAsync(string email, string password);
        UniTask<bool> TryRegisterAsync(string email, string password);
        UniTask<bool> TryLoginAsync(string email, string password);
        void Logout();

        event Action<AuthUser> OnUserChanged;
    }
}
