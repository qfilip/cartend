using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class GetAllOwners
{
    public class Handler : IAppHandler<Unit>
    {
        private readonly IAppDataStore _store;
        public Handler(IAppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(Unit _)
        {
            var data = await _store.Owners.GetAllAsync();
            var result = data.Select(OwnerDto.From).ToArray();

            return AppResult.Ok(result);
        }

        public string[] Validate(Unit _) => [];
    }
}
