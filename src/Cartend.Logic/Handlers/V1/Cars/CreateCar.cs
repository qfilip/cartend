using Cartend.Api.DataAccess;
using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Entities;
using Cartend.Dtos.V1;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class CreateCar
{
    public class Handler : IAppHandler<CreateCarRequest>
    {
        private readonly IAppDataStore _store;
        public Handler(IAppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(CreateCarRequest request)
        {
            var ownerExists = await _store.Owners.ExistsAsync(request.OwnerId);
            if (!ownerExists)
                return AppResult.NotFound($"Owner with id {request.OwnerId} not found");

            var entity = new Car
            {
                Entity = new DataAccess.Tables.CarTable
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    OwnerId = request.OwnerId
                }
            };

            _store.Cars.Add(entity);
            await _store.CommitAsync();

            return AppResult.Ok(CarDto.From(entity));
        }

        public string[] Validate(CreateCarRequest request)
        {
            return new string?[]
            {
                Validators.Car.Name.Validate(request.Name)
            } 
            .OfType<string>()
            .ToArray();
        }
    }
}
