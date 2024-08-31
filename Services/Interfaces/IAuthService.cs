using BookBase.Models;

namespace BookBase.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);

        // Other methods like RegisterAsync, LogoutAsync, etc.
    }
}
