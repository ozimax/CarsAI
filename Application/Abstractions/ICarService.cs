using myapp.Application.Common;
using myapp.Domain.Entities;

namespace myapp.Application.Abstractions;

public interface ICarService
{
    Task<IReadOnlyList<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<ServiceResult> CreateAsync(Car car);
    Task<ServiceResult> UpdateAsync(Car car);
    Task<ServiceResult> DeleteAsync(int id);
}
