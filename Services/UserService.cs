using BookBase.Data;
using BookBase.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using BookBase.Services.Interfaces;
using System.Xml;
using BookBase.DTOs;


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



     

        //public async Task<User> CreateUserAsync(UserCreateDto userDto)
        //{
        //    var user = new User(userDto.Username, userDto.Email, userDto.FirstName, userDto.LastName);

        //    user.SetPasswordHash(_passwordHasherService.HashPassword(userDto.Password));

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //    return user;

        //}



        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }



        public async Task<User?> GetUserByIdAsync(int id)
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
        public async Task<User?> GetByUsername(string username) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("User with username " + username + " does not exist");
            }

            return user;
        }
         

        public void DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
