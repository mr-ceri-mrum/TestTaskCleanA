namespace CelanA.Application.Interface;

public interface IUserService
{
    bool IsCurrentRole(string role);
    bool IsUserInRole(string role);
    string[] GetRoles();
}