using System.Net;
using CleanA.Domain.Entitys.Color;

namespace CleanA.Domain.Dtos.Car;

public class CarDto
{
    public int ColorId { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName  { get; set; }
    public int? Price { get; set; }
    public Color? Color { get; set; }
    public DateTime AddDateTime { get; set; }
}