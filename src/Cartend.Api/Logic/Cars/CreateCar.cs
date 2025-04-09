using Cartend.Api.DataAccess;
using Cartend.Api.DataAccess.Access.Stores;
using Cartend.Api.DataAccess.Entities;
using Cartend.Api.Dtos;
using Cartend.Api.Dtos.Entities;
using Cartend.Api.Logic.Abstractions;

namespace Cartend.Api.Logic;

public static class CreateCar
{
    public class Handler : IAppHandler<CreateCarRequest>
    {
        private readonly AppDataStore _store;
        public Handler(AppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(CreateCarRequest request)
        {
            var entity = new Car
            {
                Entity = new DataAccess.Tables.CarTable
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    OwnerId = request.OwnerId,
                }
            };

            _store.Cars.Add(entity);
            await _store.CommitAsync();

            return AppResult.Ok(CarDto.From(entity));
        }

        public string[] Validate(CreateCarRequest request)
        {
            return Validators.Car.Name.Validate(request.Name)
                ? []
                : [Validators.Car.Name.Error];
        }
    }
}
