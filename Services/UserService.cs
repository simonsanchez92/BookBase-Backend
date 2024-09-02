using BookBase.Data;
using BookBase.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using BookBase.Services.Interfaces;
using System.Xml;
using BookBase.DTOs;
using BookBase.Utilities;


namespace BookBase.Services
{
    public class UserService : IUserService
    { 

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;

        private readonly ILogger<UserService> _logger;


        public UserService(ApplicationDbContext context, IPasswordHasherService passwordHasherService, ILogger<UserService> logger)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
            _logger = logger;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                
                if (user == null) {
                    _logger.LogWarning($"User with id {id} was not found");
                
                }

                return user;
            }
            catch (Exception ex) {

                _logger.LogError(ex, $"An error occurred while retrieving the user with ID {id}.");
                return null;
            }

        }


        //For now method is being used internally in AuthService
        // Method is not being exposed through the controllers
        public async Task<User?> GetByUsernameAsync(string username) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("User with username " + username + " does not exist");
            }

            return user;
        }



        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                Console.WriteLine("User with email " + email + " does not exist");
            }

            return user;
        }


        public void DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
