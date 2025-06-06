﻿using Cartend.DataAccess.Entities;

namespace Cartend.DataAccess.Abstractions;

public interface ICarServiceRepository : IRepository<CarService>
{
    Task<CarService[]> GetCarServiceHistoryAsync(Guid carId);
}
