namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface IStore<TEntity>
{
    void Add(TEntity entity);
}
