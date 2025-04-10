using Cartend.Api.DataAccess;
using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Entities;
using Cartend.Dtos.V1;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class CreateCarService
{
    public class Handler : IAppHandler<CreateCarServiceRequest>
    {
        private readonly IAppDataStore _store;
        public Handler(IAppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(CreateCarServiceRequest request)
        {
            var carExists = await _store.Cars.ExistsAsync(request.CarId);
            if (!carExists)
                return AppResult.NotFound($"Car with id {request.CarId} not found");

            var entity = new CarService
            {
                Entity = new()
                {
                    Id = Guid.NewGuid(),
                    CarId = request.CarId,
                    ServicedAt = DateTime.Now,
                    ServicedBy = request.ServicedBy,
                    WorkDone = request.WorkDone
                }
            };

            _store.CarServices.Add(entity);
            await _store.CommitAsync();
            
            return AppResult.Ok(CarServiceDto.From(entity));
        }

        public string[] Validate(CreateCarServiceRequest request)
        {
            return new string?[]
            {
                Validators.CarService.ServicedBy.Validate(request.ServicedBy)
            }
            .OfType<string>()
            .ToArray();
        }
    }
}
