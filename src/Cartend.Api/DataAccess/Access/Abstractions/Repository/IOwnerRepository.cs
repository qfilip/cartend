using Cartend.Api.DataAccess.Entities;

namespace Cartend.Api.DataAccess.Access.Abstractions.Repository;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<Owner[]> GetAllAsync();
}
