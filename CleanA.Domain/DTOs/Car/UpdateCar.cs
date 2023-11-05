namespace CleanA.Domain.Dtos.Car;

public class UpdateCar
{
    public int? ColorId { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName  { get; set; }
    public int? Price { get; set; }
}