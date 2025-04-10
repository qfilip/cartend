using Cartend.Api.DataAccess;
using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Access.Abstractions.Stores;
using Cartend.DataAccess.Entities;
using Cartend.DataAccess.Tables;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace Cartend.DataAccess.Stores;

public class CarStore : ICarStore
{
    private readonly List<Car> _added = new();
    private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;
    public CarStore(IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        _accessor = accessor;
    }

    public void Add(Car car) => _added.Add(car);

    public Task<bool> ExistsAsync(Guid id) => StoreUtilities.EntityExistsAsync(id, TableNames.Car, _accessor);

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

        return await _accessor.QueryAsync(prep, read);
    }

    public void Accept(IAccessor<SqliteCommand, SqliteDataReader> accessor)
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

    public void PrepareTransaction()
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

        _accessor.AddTransactionCommandPrep(prep);
    }
}
