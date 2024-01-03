using CleanA.Domain.DTOs.User;

namespace CelanA.Application.Interface;

public interface IAuthService
{
    Task<string?> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}