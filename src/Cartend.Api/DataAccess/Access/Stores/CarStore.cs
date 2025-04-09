using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Entities;
using Cartend.Api.DataAccess.Tables;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace Cartend.Api.DataAccess.Access.Stores;

public class CarStore : ICarStore, IStoreVisitable
{
    private readonly SqliteAccessor _accessor;
    private readonly List<Car> _added = new();
    public CarStore(SqliteAccessor accessor)
    {
        _accessor = accessor;
    }

    public void Add(Car car) => _added.Add(car);

    public async Task<Car[]> GetOwnerCarsAsync(Guid ownerId)
    {
        var prep = (SqliteCommand cmd) =>
        {
            cmd.CommandText = $"SELECT * FROM {TableNames.Car} WHERE ownerId = @id";
            cmd.Parameters.AddWithValue("@id", ownerId);
        };

        var read = (SqliteDataReader reader) =>
        {
            var result = new List<CarTable>();
            while (reader.Read())
            {
                var id = reader.GetFieldValue<Guid>(nameof(CarTable.Id));
                var ownerId = reader.GetFieldValue<Guid>(nameof(CarTable.OwnerId));
                var name = reader.GetFieldValue<string>(nameof(CarTable.Name));
                result.Add(new CarTable { Id = id, OwnerId = ownerId, Name = name });
            }

            return result.Select(x => new Car { Entity = x }).ToArray();
        };

        return await _accessor.ExecuteQueryAsync(prep, read);
    }

    public void Accept(SqliteAccessor accessor)
    {
        if (_added.Count == 0) return;

        var prep = (SqliteCommand cmd) =>
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO {TableNames.Car} (id, ownerId, name) VALUES ");

            foreach (var owner in _added)
            {
                sb.Append("(@id, @ownerId, @name),");
                cmd.Parameters.AddWithValue("@id", owner.Entity.Id);
                cmd.Parameters.AddWithValue("@ownerId", owner.Entity.OwnerId);
                cmd.Parameters.AddWithValue("@name", owner.Entity.Name);
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(";");

            cmd.CommandText = sb.ToString();
            _added.Clear();
        };
        
        accessor.AddTransactionCommandPrep(prep);
    }
}
