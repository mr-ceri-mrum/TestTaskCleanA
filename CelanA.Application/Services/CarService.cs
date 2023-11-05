using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using CelanA.Application.Interface;
using CleanA.Domain.ActionResponse;
using CleanA.Domain.Dtos.Car;
using CleanA.Domain.Entitys.Car;
using CleanA.Domain.Enums;
using CleanA.Infrastructure.DbContexts;
using CleanA.Infrastructure.Helpper.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CelanA.Application.Services;

public class CarService(DataContext dbContext, UserManager<IdentityUser> userManager, IMapper mapper) : ICarService
{
    public UserManager<IdentityUser> UserManager { get; } = userManager;

    public async Task<ActionMethodResult> GetCars()
    {
        try
        {
            ParallelQuery<Car> cars = dbContext.Set<Car>()
                .Include(x => x.Color)
                .Where(x => x.StatusCar == StatusCar.Instock)
                .AsNoTracking()
                .AsParallel();
            
            ParallelQuery<CarDto> carDtos = cars.Select(mapper.Map<CarDto>);
            
            return ActionMethodResult.Success(carDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> GetCarById(int id)
    {
        try
        {
            var car = await dbContext.Set<Car>()
                .Include(x => x.Color)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (car == null)
                return ActionMethodResult.Error("Not Found");
            
            CarDto result = mapper.Map<Car, CarDto>(car);
            
            return ActionMethodResult.Success(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<ActionMethodResult> AddCar(CreateCarDto carDto)
    {
        try
        {
            Car car = new Car(carDto.BrandName, carDto.ModelName, carDto.Price, carDto.ColorId);
            await dbContext.Set<Car>().AddAsync(car);
            await dbContext.SaveChangesAsync();
            
            CarDto resulDto = mapper.Map<Car, CarDto>(car);
            
            return ActionMethodResult.Success(resulDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<ActionMethodResult> DeleteCarById(int id)
    {
        try
        {
            var car = await dbContext.Set<Car>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;
            
            if (car == null) return ActionMethodResult.Error("not found");
            
            dbContext.Cars.Remove(car);
            await dbContext.SaveChangesAsync();
            
            return ActionMethodResult.Success("Deleted Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> UpdateCarById(int id, UpdateCar updateCar)
    {
        try
        {
            var car = await dbContext.Set<Car>()
                .FirstOrDefaultAsync(x => x.Id == id && x.StatusCar == StatusCar.Instock) ?? null;
            
            if (car == null) ActionMethodResult.Error("not found");
            
            car.BrandName = updateCar.BrandName ?? car.BrandName;
            car.ColorId = updateCar.ColorId ?? car.ColorId;
            car.ModelName = updateCar.ModelName ?? car.ModelName;
            
            dbContext.Cars.Update(car);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<Car, CarDto>(car);
            return ActionMethodResult.Success(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> GetCarFilters(CarFilterDto carFilterDto)
    {
        try
        {
            var cars = dbContext.Set<Car>()
                .AsNoTracking()
                .AsParallel();
            
            cars = CarFilters.FiltersCars(cars, carFilterDto);
            
            ParallelQuery<CarDto> carDtos = cars.Select(mapper.Map<CarDto>);
            
            return ActionMethodResult.Success(carDtos);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}