using Cartend.Api.DataAccess.Entities;

namespace Cartend.Api.DataAccess.Access.Abstractions.Repository;

public interface ICarRepository : IRepository<Car>
{
    Task<Car[]> GetOwnerCarsAsync(Guid ownerId);
}
