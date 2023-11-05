using CelanA.Application.Interface;
using CleanA.Domain.ActionResponse;
using CleanA.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Color = CleanA.Domain.Entitys.Color.Color;

namespace CelanA.Application.Services;

public class ColorService(DataContext dataContext) : IColorService
{
    public async Task<ActionMethodResult> AddColor(string colorName)
    {
        try
        {
            var color = new Color(colorName);
            await dataContext.Colors.AddAsync(color);
            await dataContext.SaveChangesAsync();
            return ActionMethodResult.Success(color);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> DeleteColorById(int id)
    {
        try
        {
            var color = await dataContext.Set<Color>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;
            
            if (color == null)
                return ActionMethodResult.Error("not found color");
            
            dataContext.Colors.Remove(color);
            await dataContext.SaveChangesAsync();
            
            return ActionMethodResult.Success("Deleted success!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> UpdateColor(int id,string colorName)
    {
        try
        {
            var color = await dataContext.Set<Color>().FirstOrDefaultAsync(x => x.Id == id) ?? null;
            if (color == null)
                return ActionMethodResult.Error("not found color");
            
            color.ColorName = colorName;
            dataContext.Colors.Update(color);
            await dataContext.SaveChangesAsync();
            
            return ActionMethodResult.Success(color);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActionMethodResult> GetColors()
    {
        try
        {
            var colors = dataContext.Set<Color>()
                .AsNoTracking()
                .AsParallel();
            
            return ActionMethodResult.Success(colors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}