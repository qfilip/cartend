using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class GetOwnerCars
{
    public class Handler : IAppHandler<Guid>
    {
        private readonly IAppDataStore _store;

        public Handler(IAppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(Guid request)
        {
            var entities = await _store.Cars.GetOwnerCarsAsync(request);
            var result = entities.Select(CarDto.From).ToArray();

            return AppResult.Ok(result);
        }

        public string[] Validate(Guid request) => [];
    }
}
