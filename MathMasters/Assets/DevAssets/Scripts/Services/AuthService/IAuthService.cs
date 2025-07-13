using System.Threading.Tasks;

namespace MathMasters
{
    public interface IAuthService
    {
        AuthUser CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task InitializeAsync();
        Task RegisterAsync(string email, string password);
        Task LoginAsync(string email, string password);
        void Logout();
    }
}
