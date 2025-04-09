using Cartend.Api.DataAccess.Entities;

namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface ICarStore : IStore<Car>
{
    Task<Car[]> GetOwnerCarsAsync(Guid ownerId);
}
