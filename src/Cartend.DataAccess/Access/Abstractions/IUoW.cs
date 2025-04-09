namespace Cartend.DataAccess.Abstractions;

public interface IUoW
{
    Task CommitAsync();
}
