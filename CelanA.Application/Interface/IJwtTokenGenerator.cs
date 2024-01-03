using CleanA.Domain.Entitys.User;

namespace CelanA.Application.Interface;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}