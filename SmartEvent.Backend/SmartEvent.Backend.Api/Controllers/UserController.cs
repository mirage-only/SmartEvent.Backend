using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService): ControllerBase 
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthorizeUserResponseDto>> RegisterUser([FromBody] RegisterUserRequestDto requestDto)
    {
        try
        {
            var response = await userService.RegisterUserAsync(requestDto);
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizeUserResponseDto>> Login([FromBody] LoginUserRequestDto requestDto)
    {
        try
        {
            var response = await userService.AuthorizeUserAsync(requestDto);

            return Ok(response);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}