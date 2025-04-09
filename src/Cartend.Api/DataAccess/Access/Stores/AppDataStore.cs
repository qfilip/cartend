using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Abstractions.Repository;
using Cartend.Api.DataAccess.Access.Abstractions.Stores;
using Microsoft.Data.Sqlite;

namespace Cartend.Api.DataAccess.Access.Stores;

public class AppDataStore : IAppDataStore
{
    private readonly IOwnerStore _ownerStore;
    private readonly ICarStore _carStore;
    private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;

    public AppDataStore(IOwnerStore ownerStore, ICarStore carStore, IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        _ownerStore = ownerStore;
        _carStore = carStore;
        _accessor = accessor;
    }

    public IOwnerRepository Owners => _ownerStore;
    public ICarRepository Cars => _carStore;

    public Task CommitAsync()
    {
        _ownerStore.PrepareTransaction();
        _carStore.PrepareTransaction();
        
        return _accessor.CommitAsync();
    }
}
