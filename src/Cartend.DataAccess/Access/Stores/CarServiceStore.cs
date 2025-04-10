using Cartend.Api.DataAccess;
using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Access.Abstractions.Stores;
using Cartend.DataAccess.Entities;
using Cartend.DataAccess.Tables;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;
using System.Text.Json.Nodes;

namespace Cartend.DataAccess.Stores;

public class CarServiceStore : ICarServiceStore
{
    private readonly List<CarService> _added = new();
    private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;
    public CarServiceStore(IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        _accessor = accessor;
    }

    public void Add(CarService entity) => _added.Add(entity);

    public Task<bool> ExistsAsync(Guid id) => StoreUtilities.EntityExistsAsync(id, TableNames.CarService, _accessor);

    public async Task<CarService[]> GetCarServiceHistoryAsync(Guid carId)
    {
        var prep = (SqliteCommand cmd) =>
        {
            cmd.CommandText = $"SELECT * FROM {TableNames.CarService} WHERE CarId = @carId";
            cmd.Parameters.AddWithValue("@carId", carId);
        };

        var read = (SqliteDataReader reader) =>
        {
            var data = new List<CarServiceTable>();
            while (reader.Read())
            {
                data.Add(new CarServiceTable()
                {
                    Id = reader.GetFieldValue<Guid>(nameof(CarServiceTable.Id)),
                    CarId = reader.GetFieldValue<Guid>(nameof(CarServiceTable.CarId)),
                    ServicedBy = reader.GetFieldValue<string>(nameof(CarServiceTable.ServicedBy)),
                    ServicedAt = reader.GetFieldValue<DateTime>(nameof(CarServiceTable.ServicedAt)),
                    WorkDone = reader.GetFieldValue<string>(nameof(CarServiceTable.WorkDone)),
                });
            }

            return data.Select(x => new CarService { Entity = x }).ToArray();
        };

        return await _accessor.QueryAsync(prep, read);
    }

    public void PrepareTransaction()
    {
        if (_added.Count == 0) return;
        var prep = (SqliteCommand cmd) =>
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO {TableNames.CarService} (id, carId, servicedBy, servicedAt, workDone) VALUES ");

            foreach (var x in _added)
            {
                sb.Append("(@id, @carId, @servicedBy, @servicedAt, @workDone),");
                cmd.Parameters.AddWithValue("@id", x.Entity.Id);
                cmd.Parameters.AddWithValue("@carId", x.Entity.CarId);
                cmd.Parameters.AddWithValue("@servicedBy", x.Entity.ServicedBy);
                cmd.Parameters.AddWithValue("@servicedAt", x.Entity.ServicedAt);
                cmd.Parameters.AddWithValue("@workDone", x.Entity.WorkDone);
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(";");

            cmd.CommandText = sb.ToString();
            _added.Clear();
        };

        _accessor.AddTransactionCommandPrep(prep);
    }
}
