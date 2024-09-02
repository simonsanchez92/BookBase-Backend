using BookBase.Models;
using BookBase.DTOs;

namespace BookBase.Services.Interfaces
{
    public interface IUserService
    {

        Task<User> GetUserByIdAsync(int id);

        Task<User?> GetByUsername(string username);

        Task<List<User>> GetAllUsersAsync();

        void DeleteUserById(int id);
    }
}
