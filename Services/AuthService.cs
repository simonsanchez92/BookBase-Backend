
using BookBase.Services.Interfaces;
using BookBase.Models;
using BookBase.DTOs;
using BookBase.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using BookBase.Data;



namespace BookBase.Services
{

    //Implement an IAuthService interface and a corresponding AuthService class to handle the actual authentication logic.
    //This could include validating user credentials, generating JWT tokens, hashing passwords, and checking roles.


    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService; // for handling user data
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasherService _passwordHasherService;


        private readonly ILogger<AuthService> _logger;


        public AuthService(ApplicationDbContext context, IUserService userService, IConfiguration configuration, IPasswordHasherService passwordHasherService, ILogger<AuthService> logger)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
            _passwordHasherService = passwordHasherService;
        }



        public async Task<ServiceResult<User>> RegisterAsync(UserCreateDto userCreateDto)
        {

            //Check if username exists
            if (await _userService.GetByUsernameAsync(userCreateDto.Username) != null)
            {
                string msg = $"Username '{userCreateDto.Username}' is already taken.";

                _logger.LogWarning(msg);
                return ServiceResult<User>.FailureResult(msg);
            }

            //Check if email exists
            if (await _userService.GetByEmailAsync(userCreateDto.Email) != null)
            {
                
                string msg = $"Email '{userCreateDto.Email}' is already taken.";

                _logger.LogWarning(msg);
                return ServiceResult<User>.FailureResult(msg);
            }



            // If both checks pass, proceed to register the user

            var user = new User(userCreateDto.Username, userCreateDto.Email, userCreateDto.FirstName, userCreateDto.LastName);

            user.SetPasswordHash(_passwordHasherService.HashPassword(userCreateDto.Password));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.SuccessResult(user, "User registered successfully");
        }


        public async Task<AuthResult> LoginAsync(string username, string password)
        {

            var user = await _userService.GetByUsernameAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return new AuthResult { Success = false, Message = "Invalid Credentials." };
            }

            string token = GenerateJwtToken(user);


            return new AuthResult { Success = true, Token = token };

        }



        private bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            return _passwordHasherService.VerifyPassword(providedPassword, hashedPassword);
        }


        private string GenerateJwtToken(User user)
        {
         
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            //Key-Value pair that represent user data that can be encoded into the JWT
            //These claims are included in the payload of the token
            var claims = new[]
            {
                //Identifies the principal (User) that is the subject of the JWT
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Sub (subject)

                //User Email
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                //Unique identifier of the token, typically a GUID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT ID

            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],  //Specifies entity issuing the token
                audience: _configuration["Jwt:Audience"], //Speficies the recipient of the token
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );



            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }



}
