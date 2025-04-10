using Cartend.Api.DataAccess;
using Cartend.Api.Logic;
using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Entities;
using Cartend.DataAccess.Tables;
using Cartend.Dtos.V1;
using Cartend.Dtos.V1.Entities;
using Cartend.Logic.Abstractions;

namespace Cartend.Logic.Handlers.V1;

public static class CreateOwner
{
    public class Handler : IAppHandler<CreateOwnerRequest>
    {
        private readonly IAppDataStore _store;

        public Handler(IAppDataStore store)
        {
            _store = store;
        }

        public async Task<AppResult> Handle(CreateOwnerRequest request)
        {
            var entity = new Owner
            {
                Entity = new OwnerTable
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                }
            };

            _store.Owners.Add(entity);
            await _store.CommitAsync();

            return AppResult.Ok(OwnerDto.From(entity));
        }

        public string[] Validate(CreateOwnerRequest request)
        {
            return new string?[]
            {
                Validators.Owner.Name.Validate(request.Name)
            }
            .OfType<string>()
            .ToArray();
        }
    }
}
