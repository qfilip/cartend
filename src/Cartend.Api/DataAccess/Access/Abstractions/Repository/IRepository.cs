namespace Cartend.Api.DataAccess.Access.Abstractions.Repository;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);
}
