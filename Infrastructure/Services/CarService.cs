using Microsoft.EntityFrameworkCore;
using myapp.Application.Abstractions;
using myapp.Application.Common;
using myapp.Domain.Entities;
using myapp.Infrastructure.Persistence;

namespace myapp.Infrastructure.Services;

public class CarService(ApplicationDbContext dbContext) : ICarService
{
    public async Task<IReadOnlyList<Car>> GetAllAsync()
    {
        return await dbContext.Cars
            .AsNoTracking()
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await dbContext.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ServiceResult> CreateAsync(Car car)
    {
        dbContext.Cars.Add(car);
        await dbContext.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Car car)
    {
        var exists = await dbContext.Cars.AnyAsync(c => c.Id == car.Id);
        if (!exists)
        {
            return ServiceResult.Fail("Car not found.");
        }

        dbContext.Cars.Update(car);
        await dbContext.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var car = await dbContext.Cars.FindAsync(id);
        if (car is null)
        {
            return ServiceResult.Fail("Car not found.");
        }

        try
        {
            dbContext.Cars.Remove(car);
            await dbContext.SaveChangesAsync();
            return ServiceResult.Ok();
        }
        catch (DbUpdateException)
        {
            return ServiceResult.Fail("Car cannot be deleted because related sales exist.");
        }
    }
}
