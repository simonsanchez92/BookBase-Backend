using BookBase.Models;

namespace BookBase.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user, string plainTextPassword);
        //User? GetUserById(int id);
        Task<User> GetUserByIdAsync(int id);

        Task<User?> GetByUsername(string username);

        Task<User> GetUsers();

        void DeleteUserById(int id);
    }
}
