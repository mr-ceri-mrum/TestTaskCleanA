using CelanA.Application.Interface;
using CleanA.Domain.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanA.API.Controllers;

public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
        
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {

        var result = await authService.Register(model);
       
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await authService.Login(model);
        
        return Ok(loginResponse);
    }
    
    [Authorize]
    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole(string email, string roleName)
    {
        var result = await authService.AddedRole(email, roleName);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("GetUserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = await authService.GetUser();
        return Ok(userId);
    }
    
}