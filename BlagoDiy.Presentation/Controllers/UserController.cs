using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlagoDiy.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService userService;
    private readonly AchievementService achievementService;

    public UserController(UserService userService, AchievementService achievementService)
    {
        this.userService = userService;
        this.achievementService = achievementService;
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
        
        
        var updatedUser = await userService.GetUserByIdAsync(id);

        return Ok(updatedUser);
    }

    [HttpGet("achievements")]
    public async Task<IActionResult> GetAchievements()
    {
        var achievements = await achievementService.GetAllAchievementsAsync();

        return Ok(achievements);
    }

    [HttpPost("achievements")]
    public async Task<IActionResult> CreateAchievement([FromBody] AchievementPost achievementDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var achievement = await achievementService.AddAchievement(achievementDto);

        return Ok();
    }
    
    [HttpGet("achievements/{userId}")]
    public async Task<IActionResult> GetUserAchievements(int userId)
    {
        var achievements = await achievementService.GetUserAchievements(userId);

        if (achievements == null)
        {
            return NotFound();
        }

        return Ok(achievements);
    }

    
    
    public record UserLogin
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}