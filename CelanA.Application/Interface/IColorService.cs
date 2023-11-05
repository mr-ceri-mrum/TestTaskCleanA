using CleanA.Domain.ActionResponse;

namespace CelanA.Application.Interface;

public interface IColorService
{
    Task<ActionMethodResult> AddColor(string colorName);
    Task<ActionMethodResult> DeleteColorById(int id);
    Task<ActionMethodResult> UpdateColor(int id, string colorName);
    Task<ActionMethodResult> GetColors();
}