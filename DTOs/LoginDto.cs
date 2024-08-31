using System.ComponentModel.DataAnnotations;

namespace BookBase.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        
        [Required] 
        public string Password { get; set; }
    }
}
