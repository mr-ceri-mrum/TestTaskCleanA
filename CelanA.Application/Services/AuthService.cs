using System.Security.Claims;
using AutoMapper;
using CelanA.Application.Interface;
using CleanA.Domain.ActionResponse;
using CleanA.Domain.DTOs.User;
using CleanA.Domain.Entitys.User;
using CleanA.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CelanA.Application.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    
    
    public AuthService(DataContext db, IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _db = db;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
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
                
                return "Good is registered";
        
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
    [Authorize]
    public async Task<ActionMethodResult> AddedRole(string email, string roleName)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user == null) 
            return ActionMethodResult.Error("not Found", "not Found", StatusCodes.Status404NotFound);
        
        if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(roleName.ToUpper())).GetAwaiter().GetResult();
        }
            
        await _userManager.AddToRoleAsync(user, roleName.ToUpper());
        return ActionMethodResult.Success($@"added for {user.Email} {roleName}");

    }

    public async Task<ActionMethodResult> GetUser()
    {
        try
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return ActionMethodResult.Error("Invalid user ID");
            }
            
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId.ToString());
            
            if (user == null)
            {
                return ActionMethodResult.Error("Not Found");
            }
            
            var userDto = _mapper.Map<ApplicationUser, UserDto>(user);
            userDto.UserRole = await _userManager.GetRolesAsync(user) as List<string>;
            return ActionMethodResult.Success(userDto);
        }
        catch (Exception ex)
        {
            // Логирование исключения
            return ActionMethodResult.Error("An error occurred: " + ex.Message);
        }
    }

    private Guid GetUserId()
    {
        if (!(_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false)) 
            return Guid.Empty;
        
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            return userId;
        
        return Guid.Empty;
    }

    
}