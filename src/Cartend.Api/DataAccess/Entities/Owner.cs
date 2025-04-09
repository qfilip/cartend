using Cartend.Api.DataAccess.Tables;

namespace Cartend.Api.DataAccess.Entities;

public class Owner : IEntityBase<OwnerTable, OwnerRelations>
{
    public OwnerTable Entity { get; set; } = new();
    public OwnerRelations Relations { get; set; } = new();
}

public class OwnerRelations
{
    public Car[] Cars { get; set; } = Array.Empty<Car>();
}
