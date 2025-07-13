using System.Threading.Tasks;

namespace MathMasters
{
    public interface IAuthService
    {
        AuthUser CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task InitializeAsync();
        Task<bool> TryRegisterOrLoginAsync(string email, string password);
        Task<bool> TryRegisterAsync(string email, string password);
        Task<bool> TryLoginAsync(string email, string password);
        void Logout();
    }
}
