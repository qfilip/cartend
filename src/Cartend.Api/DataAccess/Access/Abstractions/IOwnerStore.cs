using Cartend.Api.DataAccess.Entities;

namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface IOwnerStore : IStore<Owner>
{
    Task<Owner[]> GetAllAsync();
}
