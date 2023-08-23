using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Route("Panel/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user/register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationViewModel userRegistration)
        {
            var userResult = await _userService.RegisterUserAsync(new Application.Models.UserDto { });
            return userResult <=  0 ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("user/token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] UserLoginViewModel userLogin)
        {
            return !await _userService.ValidateUserAsync(userLogin.Username, userLogin.Password)
                ? Unauthorized()
                : Ok(new { Token = await _userService.CreateTokenAsync() });
        }

        [HttpPost("user/token/refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> UserRefreshToken([Required, FromBody] UserRefreshTokenViewModel refreshToken)
        {
            var response = await _userService.RefreshTokenAsync(refreshToken.Token);
            if (response.HasError)
                return BadRequest(response.Error);
            return Ok(response.Token);
        }
    }
}
