using CleanA.Domain.ActionResponse;
using CleanA.Domain.DTOs.User;

namespace CelanA.Application.Interface;

public interface IAuthService
{
    Task<string?> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<ActionMethodResult> AddedRole(string email, string roleName);
    Task<ActionMethodResult> GetUser();
}