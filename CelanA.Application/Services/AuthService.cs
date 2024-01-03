using CelanA.Application.Interface;
using CleanA.Domain.DTOs.User;
using CleanA.Domain.Entitys.User;
using CleanA.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;

namespace CelanA.Application.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    
    public AuthService(DataContext db, IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<string?> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            Name = registrationRequestDto.Email,
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
        };
        
        try
        {
            var result = await _userManager.CreateAsync(user,registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                
                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    Id = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };
                
                return "Succesu";
        
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);
        
        if(user==null || isValid == false)
        {
            return new LoginResponseDto() { User = null,Token="" };
        }
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user,roles);
        
        UserDto userDTO = new()
        {
            Email = user.Email,
            Id = user.Id,
            Name = user.Email,
        };
        
        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };

        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        throw new NotImplementedException();
    }
}