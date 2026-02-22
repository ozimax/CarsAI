using Microsoft.EntityFrameworkCore;
using myapp.Application.Abstractions;
using myapp.Application.Common;
using myapp.Domain.Entities;
using myapp.Infrastructure.Persistence;

namespace myapp.Infrastructure.Services;

public class SoldCarService(ApplicationDbContext dbContext) : ISoldCarService
{
    public async Task<IReadOnlyList<SoldCar>> GetAllAsync()
    {
        return await dbContext.SoldCars
            .AsNoTracking()
            .Include(s => s.Car)
            .Include(s => s.Customer)
            .OrderByDescending(s => s.Id)
            .ToListAsync();
    }

    public async Task<ServiceResult> CreateAsync(SoldCar soldCar)
    {
        var carExists = await dbContext.Cars.AnyAsync(c => c.Id == soldCar.CarId);
        if (!carExists)
        {
            return ServiceResult.Fail("Selected car was not found.");
        }

        var customerExists = await dbContext.Customers.AnyAsync(c => c.Id == soldCar.CustomerId);
        if (!customerExists)
        {
            return ServiceResult.Fail("Selected customer was not found.");
        }

        dbContext.SoldCars.Add(soldCar);
        await dbContext.SaveChangesAsync();
        return ServiceResult.Ok();
    }
}
