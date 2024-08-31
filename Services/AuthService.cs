
using BookBase.Services.Interfaces;
using BookBase.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;



namespace BookBase.Services
{

    //Implement an IAuthService interface and a corresponding AuthService class to handle the actual authentication logic.
    //This could include validating user credentials, generating JWT tokens, hashing passwords, and checking roles.


    public class AuthService : IAuthService
    {
        private readonly IUserService _userService; // for handling user data
        private readonly IConfiguration _configuration;



        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        private bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
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



        public async Task<AuthResult> LoginAsync(string username, string password) {

            var user = await _userService.GetByUsername(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return new AuthResult { Success = false, Message = "Invalid Credentials."};
            }

            string token = GenerateJwtToken(user);


            return new AuthResult { Success = true, Token = token };
        
        }
    }



}
