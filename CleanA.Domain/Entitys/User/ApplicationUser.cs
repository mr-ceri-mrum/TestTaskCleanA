using Microsoft.AspNetCore.Identity;

namespace CleanA.Domain.Entitys.User;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}