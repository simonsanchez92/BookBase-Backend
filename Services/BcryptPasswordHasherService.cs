using BookBase.Services.Interfaces;
using BCrypt.Net;

namespace BookBase.Services
{
    public class BcryptPasswordHasherService : IPasswordHasherService
    {

        public string HashPassword(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

        }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }
    }
}
