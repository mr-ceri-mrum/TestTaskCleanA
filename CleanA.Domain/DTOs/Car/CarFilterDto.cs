using CleanA.Domain.Enums;

namespace CleanA.Domain.Dtos.Car;

public class CarFilterDto
{
    public int? ColorId { get; set; }
    public int? BeforePrice { get; set; }
    public int? AfterPrice { get; set; }
    public StatusCar? StatusCar { get; set; }
}