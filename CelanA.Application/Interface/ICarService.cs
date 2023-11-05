using System.Security.Claims;
using CleanA.Domain.ActionResponse;
using CleanA.Domain.Dtos.Car;

namespace CelanA.Application.Interface;

public interface ICarService
{
    Task<ActionMethodResult> GetCars();
    Task<ActionMethodResult> GetCarById(int id);
    Task<ActionMethodResult> DeleteCarById(int id);
    Task<ActionMethodResult> UpdateCarById(int id, UpdateCar updateCar);
    Task<ActionMethodResult> AddCar( CreateCarDto carDto);
    Task<ActionMethodResult> GetCarFilters(CarFilterDto carFilterDto);
}