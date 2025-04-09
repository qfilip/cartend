using Cartend.Api.DataAccess;
using Cartend.Api.DataAccess.Access.Abstractions.Stores;
using Cartend.Api.DataAccess.Access.Stores;
using Cartend.Api.DataAccess.Entities;
using Cartend.Api.DataAccess.Tables;
using Cartend.Api.Dtos;
using Cartend.Api.Dtos.Entities;
using Cartend.Api.Logic.Abstractions;

namespace Cartend.Api.Logic;

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
            return Validators.Owner.Name.Validate(request.Name)
                ? []
                : [Validators.Owner.Name.Error];
        }
    }
}
