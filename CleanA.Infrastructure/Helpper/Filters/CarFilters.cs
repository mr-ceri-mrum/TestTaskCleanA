using System.Diagnostics;
using CleanA.Domain.Dtos.Car;
using CleanA.Domain.Entitys.Car;

namespace CleanA.Infrastructure.Helpper.Filters;

public static class CarFilters
{
    public static  ParallelQuery<Car> FiltersCars(ParallelQuery<Car> cars, CarFilterDto carFilterDto)
    {
        if (carFilterDto.StatusCar != null)
            cars = cars.Where(x => x.StatusCar == carFilterDto.StatusCar);
        
        if(carFilterDto.ColorId != null)
            cars = cars.Where(x => x.ColorId == carFilterDto.ColorId);

        if (carFilterDto is { StatusCar: not null, ColorId: not null })
            cars = cars.Where(x => x.StatusCar == carFilterDto.StatusCar && x.ColorId == carFilterDto.ColorId);
        
        if (carFilterDto.AfterPrice != null)
            cars = cars.Where(x => x.Price >= carFilterDto.AfterPrice);
        
        if (carFilterDto.BeforePrice != null)
            cars = cars.Where(x => x.Price <= carFilterDto.BeforePrice);
        
        if (carFilterDto is { BeforePrice: not null, AfterPrice: not null })
            cars = cars.Where(x => x.Price <= carFilterDto.BeforePrice && x.Price >= carFilterDto.AfterPrice);
        
        return cars;
    }

    
}