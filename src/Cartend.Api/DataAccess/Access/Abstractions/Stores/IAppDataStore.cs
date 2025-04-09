using Cartend.Api.DataAccess.Access.Abstractions.Repository;

namespace Cartend.Api.DataAccess.Access.Abstractions.Stores;

public interface IAppDataStore : IUoW
{
    IOwnerRepository Owners { get; }
    ICarRepository Cars { get; }
}
