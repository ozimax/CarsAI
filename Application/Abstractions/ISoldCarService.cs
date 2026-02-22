using myapp.Application.Common;
using myapp.Domain.Entities;

namespace myapp.Application.Abstractions;

public interface ISoldCarService
{
    Task<IReadOnlyList<SoldCar>> GetAllAsync();
    Task<ServiceResult> CreateAsync(SoldCar soldCar);
}
