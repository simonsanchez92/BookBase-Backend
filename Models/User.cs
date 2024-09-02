namespace BookBase.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }



        public User() { }

        public User(string username, string email, string firstName, string lastName)
        {
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public User(int id, string username, string email, string firstName, string lastName, string passwordHash)
        {
            Id = id;
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
        }

        public void SetPasswordHash(string hashedPassword)
        {
            PasswordHash = hashedPassword; 
        }
    }
}
