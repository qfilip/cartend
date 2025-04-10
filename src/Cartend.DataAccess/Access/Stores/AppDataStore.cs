using Cartend.DataAccess.Abstractions;
using Microsoft.Data.Sqlite;

namespace Cartend.DataAccess.Stores;

public class AppDataStore : IAppDataStore
{
    private readonly IOwnerStore _ownerStore;
    private readonly ICarStore _carStore;
    private readonly ICarServiceStore _carServiceStore;
    private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;
    public AppDataStore(
        IOwnerStore ownerStore,
        ICarStore carStore,
        ICarServiceStore carServiceStore,
        IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        _ownerStore = ownerStore;
        _carStore = carStore;
        _carServiceStore = carServiceStore;
        _accessor = accessor;
    }

    public IOwnerRepository Owners => _ownerStore;
    public ICarRepository Cars => _carStore;
    public ICarServiceRepository CarServices => _carServiceStore;

    public Task CommitAsync()
    {
        _ownerStore.PrepareTransaction();
        _carStore.PrepareTransaction();
        _carServiceStore.PrepareTransaction();

        return _accessor.CommitAsync();
    }
}
