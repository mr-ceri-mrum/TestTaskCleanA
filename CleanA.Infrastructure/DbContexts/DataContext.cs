using CleanA.Domain.Entitys.Car;
using CleanA.Domain.Entitys.Color;
using CleanA.Domain.Entitys.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanA.Infrastructure.DbContexts;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<WalletUser> WalletUsers { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Car> Cars { get; set; }
}