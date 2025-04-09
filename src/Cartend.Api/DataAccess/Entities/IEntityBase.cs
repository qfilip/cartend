namespace Cartend.Api.DataAccess.Entities;

public interface IEntityBase<TEntity, TRelations>
{
    public TEntity Entity { get; set; }
    public TRelations Relations { get; set; }
}
