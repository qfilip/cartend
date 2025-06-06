﻿using Cartend.DataAccess.Tables;

namespace Cartend.DataAccess.Entities;

public class Car : IEntityBase<CarTable, CarRelations>
{
    public CarTable Entity { get; set; } = new();
    public CarRelations Relations { get; set; } = new();
}

public class CarRelations
{
    public Owner? Owner { get; set; }
}
