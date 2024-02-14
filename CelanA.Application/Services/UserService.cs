using System.Security.Claims;
using CelanA.Application.Interface;
using CleanA.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http;

namespace CelanA.Application.Services;

public class UserService(DataContext db, IHttpContextAccessor httpContextAccessor) : IUserService
{
    public readonly DataContext _db = db;
    
    public bool IsCurrentRole(string role)
    {
        // TODO: 'HttpContext' may be null here.
        return httpContextAccessor.HttpContext.User.Claims
            .Any(claim => claim.Type == ClaimTypes.Role && claim.Value.Equals(role, StringComparison.OrdinalIgnoreCase));
    }
    
    
    public bool IsUserInRole(string role)
    {
        var userName = httpContextAccessor.HttpContext.User.Identity.Name;

        var user = _db.Users
            .FirstOrDefault(s => s.UserName.Equals(userName));

        var userRoles = _db.UserRoles
            .Where(s => s.UserId.Equals(user.Id))
            .Select(s => s.RoleId)
            .ToList();

        bool isInRole = _db.Roles
            .Any(s => userRoles.Contains(s.Id) && s.NormalizedName.Equals(role));

        return isInRole;
    }
    
    
    public string[] GetRoles()
    {
        var userName = httpContextAccessor.HttpContext.User.Identity.Name;

        var user = _db.Users
            .FirstOrDefault(s => s.UserName.Equals(userName));

        var userRoles = _db.UserRoles
            .Where(s => s.UserId.Equals(user.Id))
            .Select(s => s.RoleId)
            .ToList();

        string[] roles = _db.Roles
            .Where(s => userRoles.Contains(s.Id))
            .Select(s => s.NormalizedName)
            .ToArray();

        return roles;
    }
}