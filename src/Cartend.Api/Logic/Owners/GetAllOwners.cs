using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Stores;
using Cartend.Api.Dtos.Entities;
using Cartend.Api.Logic.Abstractions;

namespace Cartend.Api.Logic;

public static class GetAllOwners
{
    public class Handler : IAppHandler<Unit>
    {
        private readonly AppDataStore _store;
        public Handler(AppDataStore store)
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
