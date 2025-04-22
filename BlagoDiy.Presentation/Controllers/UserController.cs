using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
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
        
        var user = await userService.CreateUserAsync(userDto);

        
        return Ok(user);

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
        
        

        return Ok(user);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteUserAsync(id);
        
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserPost userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await userService.GetUserByIdAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }
        
        await userService.UpdateUserAsync(userDto, id);
        
        return NoContent();
    }
}

public record UserLogin
{
    public string Email { get; init; }
    public string Password { get; init; }
}