namespace Cartend.DataAccess.Abstractions;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);
}
