using AZFlow360.API.DTOs;
using AZFlow360.Application.Features.Auth.Commands;
using AZFlow360.Application.Features.Auth.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AZFlow360.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var command = new RegisterCommand
            {
                Username = request.Username,
                Password = request.Password,
                FullName = request.FullName,
                RoleID = request.RoleID
            };

            var userId = await Mediator.Send(command);
            return Ok(new { UserId = userId, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var query = new LoginQuery
            {
                Username = request.Username,
                Password = request.Password
            };

            var token = await Mediator.Send(query);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}