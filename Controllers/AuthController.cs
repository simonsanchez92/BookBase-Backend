using BookBase.DTOs;
using BookBase.Models;
using BookBase.Services;
using BookBase.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookBase.Controllers
{

    //The AuthController will handle all authentication-related operations, such as login, logout,
    //registration, and token refresh(if using JWT tokens).



    //Single Responsibility Principle(SRP) : Each controller should have a single responsibility.
    //- UserController: user management(CRUD operations)
    //- AuthController: authentication handling.


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreateDto userCreateDto)
        {
            var res = await _authService.RegisterAsync(userCreateDto);

            if (!res.Success)
            {
                return Conflict(new { res.Message });
            }

            return CreatedAtAction(nameof(Register), new {id = res.Data.Id}, res.Data);
        }



        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto.Username, loginDto.Password);

            if (!result.Success)
            {
                return new UnauthorizedResult();
            }

            return Ok(new {Token = result.Token});
        }
    }
}
