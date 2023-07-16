using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationViewModel userRegistration)
        {

            var userResult = await _userService.RegisterUserAsync(new Application.Models.UserDto { });
            return userResult <=  0 ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginViewModel user)
        {
            return !await _userService.ValidateUserAsync(user.Username, user.Password)
                ? Unauthorized()
                : Ok(new { Token = await _userService.CreateTokenAsync() });
        }
    }
}
