using Cartend.Api.DataAccess.Access.Abstractions;

namespace Cartend.Api.DataAccess.Access.Stores;

public class AppDataStore : IUoW
{
    private readonly SqliteAccessor _accessor;
    private readonly OwnerStore _ownerStore;
    private readonly CarStore _carStore;

    public AppDataStore(OwnerStore ownerStore, CarStore carStore, SqliteAccessor accessor)
    {
        _ownerStore = ownerStore;
        _carStore = carStore;
        _accessor = accessor;
    }

    public IOwnerStore Owners => _ownerStore;
    public ICarStore Cars => _carStore;

    public Task<int> CommitAsync()
    {
        _accessor.Visit(_ownerStore);
        _accessor.Visit(_carStore);
        
        return _accessor.ExecuteTransactionAsync();
    }
}
