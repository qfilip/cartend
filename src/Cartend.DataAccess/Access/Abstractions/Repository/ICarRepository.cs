using Cartend.DataAccess.Entities;

namespace Cartend.DataAccess.Abstractions;

public interface ICarRepository : IRepository<Car>
{
    Task<Car[]> GetOwnerCarsAsync(Guid ownerId);
}
