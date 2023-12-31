namespace CleanA.Domain.DTOs.User;

public class UserDto
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public List<string>? UserRole { get; set; }
}