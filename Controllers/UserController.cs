using Microsoft.AspNetCore.Mvc;
using BookBase.Models;
using BookBase.Services.Interfaces;
using BookBase.DTOs;

namespace BookBase.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            

            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {

            var users = await _userService.GetAllUsersAsync();


            if (users == null || !users.Any())
            {
                return NotFound();
            }


            //Shaping response via DTO
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            });


            return Ok(userDtos);
        }


      }
}
