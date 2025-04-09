namespace Cartend.DataAccess.Abstractions;

public interface IAppDataStore : IUoW
{
    IOwnerRepository Owners { get; }
    ICarRepository Cars { get; }
}
