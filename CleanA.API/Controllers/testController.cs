using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanA.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class testController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> AddColor()
    {
        return Ok("aaaa");
    }
}