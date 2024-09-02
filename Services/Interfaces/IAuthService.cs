using BookBase.Models;
using BookBase.DTOs;

namespace BookBase.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserCreateDto userDto);

        Task<AuthResult> LoginAsync(string username, string password);

        // Other methods like ChangePassword etc.
    }
}
