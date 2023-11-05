using CelanA.Application.Interface;
using CleanA.Domain.Dtos.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanA.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class CarController(ICarService carService) : ControllerBase
{
    /// <summary>
    /// Добавление машины
    /// </summary>
    /// <param name="carDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddCar([FromForm]CreateCarDto carDto)
    {
        var result = await carService.AddCar(carDto);
        return Ok(result);
    }
    
    /// <summary>
    /// Получение всех машин
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCars()
    {
        var result = await carService.GetCars();
        return Ok(result);
    }
    
    /// <summary>
    /// Получить машину
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCarById(int id)
    {
        var result = await carService.GetCarById(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCarById(int id, [FromForm]UpdateCar updateCar)
    {
        var result = await carService.UpdateCarById(id, updateCar);
        return Ok(result);
    }

    /// <summary>
    /// Удаление машины
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteCarById(int id)
    {
        var result = await carService.DeleteCarById(id);
        return Ok(result);
    }
    
    /// <summary>
    /// фильтрация машин 
    /// </summary>
    /// <param name="carFilterDto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCarFilters([FromQuery]CarFilterDto carFilterDto)
    {
        var result = await carService.GetCarFilters(carFilterDto);
        return Ok(result);
    }
}