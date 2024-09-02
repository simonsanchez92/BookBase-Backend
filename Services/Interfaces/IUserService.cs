using BookBase.Models;
using BookBase.DTOs;

namespace BookBase.Services.Interfaces
{
    public interface IUserService
    {

        Task<User> GetByIdAsync(int id);

        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByEmailAsync(string username);



        Task<List<User>> GetAllUsersAsync();

        void DeleteUserById(int id);
    }
}
