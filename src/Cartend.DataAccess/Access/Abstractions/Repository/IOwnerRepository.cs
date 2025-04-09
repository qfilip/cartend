using Cartend.DataAccess.Entities;

namespace Cartend.DataAccess.Abstractions;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<Owner[]> GetAllAsync();
}
