using Cartend.Api.DataAccess.Access;
using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Abstractions.Stores;
using Cartend.Api.DataAccess.Entities;
using Cartend.Api.Logic.Abstractions;
using Microsoft.Data.Sqlite;

namespace Cartend.Api.Logic;

public static class TestTransaction
{
    public class Handler : IAppHandler<bool>
    {
        private readonly IAppDataStore _store;
        private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;

        public Handler(IAppDataStore store, IAccessor<SqliteCommand, SqliteDataReader> accessor)
        {
            _store = store;
            _accessor = accessor;
        }
        public async Task<AppResult> Handle(bool fail)
        {
            var clearData = (SqliteCommand cmd) =>
            {
                cmd.CommandText =
                $"""
                    DELETE FROM {TableNames.Car};    
                    DELETE FROM {TableNames.Owner};
                """;
            };

            await _accessor.CommandAsync(clearData);

            var owner1 = new Owner
            {
                Entity = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "owner-1"
                }
            };

            _store.Owners.Add(owner1);
            await _store.CommitAsync();

            var owner2 = new Owner
            {
                Entity = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "owner-2"
                }
            };
            var car1 = new Car
            {
                Entity = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "car-1",
                    OwnerId = fail ? Guid.NewGuid() : owner1.Entity.Id,
                }
            };

            _store.Owners.Add(owner2);
            _store.Cars.Add(car1);

            await _store.CommitAsync();
            
            return AppResult.Ok();
        }

        public string[] Validate(bool _) => [];
    }
}
