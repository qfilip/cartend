using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class GetCarServices
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
            var entities = await _store.CarServices.GetCarServiceHistoryAsync(request);
            var dtos = entities.Select(CarServiceDto.From).ToArray();

            return AppResult.Ok(dtos);
        }

        public string[] Validate(Guid request) => [];
    }
}
