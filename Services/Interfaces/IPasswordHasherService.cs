namespace BookBase.Services.Interfaces
{
    public interface IPasswordHasherService
    {
        string HashPassword(string plainTextPassword);
        bool VerifyPassword(string plainTextPassword, string hashedPassword);

    }
}
