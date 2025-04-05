using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.AspNetCore.Mvc;

namespace BlagoDiy.Controllers;

[ApiController]
[Route("api/users")]

public class UserController : ControllerBase
{
    
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> CreateUser([FromBody] UserPost userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userGetDto = await userService.CreateUserAsync(userDto);

        var token = JwtHelper.GenerateToken(userGetDto);
        
        return Ok(new { token=token });

    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await userService.LoginAsync(userLogin.Email, userLogin.Password);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var token = JwtHelper.GenerateToken(user);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { token=token });
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteUserAsync(id);
        
        return NoContent();
    }
}

public record UserLogin
{
    public string Email { get; init; }
    public string Password { get; init; }
}