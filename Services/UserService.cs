using BookBase.Data;
using BookBase.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using BookBase.Services.Interfaces;


namespace BookBase.Services
{
    public class UserService : IUserService
    { 

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;


        public UserService(ApplicationDbContext context, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService; 
        }



        public async Task<User> CreateUserAsync(User user, string plainTextPassword)
        {
            user.SetPasswordHash(_passwordHasherService.HashPassword(plainTextPassword));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user  = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                Console.WriteLine("User with id "+ id + " not found");
            }

            return user;

        }


        //For now method is being used internally in AuthService
        // Method is not being exposed through the controllers
        public async Task<User?> GetByUsername(string username) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("User with username " + username + " does not exist");
            }

            return user;
        }
         
          public async  Task<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
