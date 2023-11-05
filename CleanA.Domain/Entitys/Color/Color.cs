using System.ComponentModel.DataAnnotations;

namespace CleanA.Domain.Entitys.Color;

public class Color
{
    public Color(string colorName)
    {
        ColorName = colorName;
    }
    [Key]
    public int Id { get; set; }
    public string ColorName { get; set; }
}