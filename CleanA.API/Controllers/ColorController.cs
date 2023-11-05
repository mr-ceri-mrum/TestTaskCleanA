using CelanA.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanA.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ColorController(IColorService colorService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddColor(string colorName)
    {
        var result = await colorService.AddColor(colorName);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteColorById(int id)
    {
        var result = await colorService.DeleteColorById(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateColor(int id, string colorName)
    {
        var result = await colorService.UpdateColor(id, colorName);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetColors()
    {
        var result = await colorService.GetColors();
        return Ok(result);
    }
}