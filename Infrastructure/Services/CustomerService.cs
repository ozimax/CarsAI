using Microsoft.EntityFrameworkCore;
using myapp.Application.Abstractions;
using myapp.Application.Common;
using myapp.Domain.Entities;
using myapp.Infrastructure.Persistence;

namespace myapp.Infrastructure.Services;

public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
{
    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        return await dbContext.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ServiceResult> CreateAsync(Customer customer)
    {
        var emailExists = await dbContext.Customers
            .AnyAsync(c => c.Email == customer.Email);
        if (emailExists)
        {
            return ServiceResult.Fail("Could not save customer. Make sure the email is unique.");
        }

        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Customer customer)
    {
        var existing = await dbContext.Customers.FindAsync(customer.Id);
        if (existing is null)
        {
            return ServiceResult.Fail("Customer not found.");
        }

        var emailExists = await dbContext.Customers
            .AnyAsync(c => c.Email == customer.Email && c.Id != customer.Id);
        if (emailExists)
        {
            return ServiceResult.Fail("Could not update customer. Make sure the email is unique.");
        }

        existing.Name = customer.Name;
        existing.Email = customer.Email;

        await dbContext.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var customer = await dbContext.Customers.FindAsync(id);
        if (customer is null)
        {
            return ServiceResult.Fail("Customer not found.");
        }

        try
        {
            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return ServiceResult.Ok();
        }
        catch (DbUpdateException)
        {
            return ServiceResult.Fail("Customer cannot be deleted because related sales exist.");
        }
    }
}
