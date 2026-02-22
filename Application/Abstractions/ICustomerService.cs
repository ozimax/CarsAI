using myapp.Application.Common;
using myapp.Domain.Entities;

namespace myapp.Application.Abstractions;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<ServiceResult> CreateAsync(Customer customer);
    Task<ServiceResult> UpdateAsync(Customer customer);
    Task<ServiceResult> DeleteAsync(int id);
}
