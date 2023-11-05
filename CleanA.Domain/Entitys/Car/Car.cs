using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanA.Domain.Enums;

namespace CleanA.Domain.Entitys.Car;

public class Car
{
    public Car(string brandName, string modelName,int price, int colorId)
    {
        BrandName = brandName;
        ModelName = modelName;
        Price = price;
        ColorId = colorId;
    }
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Color))]
    public int ColorId { get; set; }

    public virtual Color.Color Color { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public StatusCar StatusCar { get; set; } = StatusCar.Instock;
    public int Price { get; set; }
    public DateTime AddedTime { get; set; } = DateTime.UtcNow;

}