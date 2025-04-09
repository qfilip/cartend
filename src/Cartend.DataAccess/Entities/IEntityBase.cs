namespace Cartend.DataAccess.Entities;

public interface IEntityBase<TEntity, TRelations>
{
    public TEntity Entity { get; set; }
    public TRelations Relations { get; set; }
}
