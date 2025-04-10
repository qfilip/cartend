using Cartend.DataAccess.Tables;

namespace Cartend.DataAccess.Entities;

public class CarService : IEntityBase<CarServiceTable, CarServiceRelations>
{
    public CarServiceTable Entity { get; set; } = new();
    public CarServiceRelations Relations { get; set; } = new ();
}

public class CarServiceRelations
{
    public Car? Car { get; set; }
}
