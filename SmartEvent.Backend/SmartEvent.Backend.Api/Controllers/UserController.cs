using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UserController(IUserService userService): BaseApiController
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto requestDto)
    {
            var response = await userService.RegisterUserAsync(requestDto);
            
            return HandleResult(response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto requestDto)
    {
        var response = await userService.AuthorizeUserAsync(requestDto);

        return HandleResult(response);
    }
}