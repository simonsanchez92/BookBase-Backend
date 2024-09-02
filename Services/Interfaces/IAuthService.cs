using BookBase.Models;
using BookBase.DTOs;
using BookBase.Utilities;

namespace BookBase.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<User>> RegisterAsync(UserCreateDto userDto);

        Task<AuthResult> LoginAsync(string username, string password);

        // Other methods like ChangePassword etc.
    }
}
