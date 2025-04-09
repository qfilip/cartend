using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Abstractions.Stores;
using Cartend.Api.DataAccess.Entities;
using Cartend.Api.DataAccess.Tables;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace Cartend.Api.DataAccess.Access.Stores;

public class OwnerStore : IOwnerStore
{
    private readonly List<Owner> _added = new();
    private readonly IAccessor<SqliteCommand, SqliteDataReader> _accessor;
    public OwnerStore(IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        _accessor = accessor;
    }

    public async Task<Owner[]> GetAllAsync()
    {
        var prep = (SqliteCommand cmd) =>
        {
            cmd.CommandText = $"SELECT * FROM {TableNames.Owner}";
        };

        var read = (SqliteDataReader reader) =>
        {
            var result = new List<OwnerTable>();
            while (reader.Read())
            {
                var id = reader.GetFieldValue<Guid>(nameof(OwnerTable.Id));
                var name = reader.GetFieldValue<string>(nameof(OwnerTable.Name));
                result.Add(new OwnerTable { Id = id, Name = name });
            }

            return result.Select(x => new Owner { Entity = x }).ToArray();
        };

        return await _accessor.QueryAsync(prep, read);
    }

    public void Add(Owner owner) => _added.Add(owner);

    public void PrepareTransaction()
    {
        if (_added.Count == 0) return;
        var prep = (SqliteCommand cmd) =>
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO {TableNames.Owner} (id, name) VALUES ");

            foreach (var owner in _added)
            {
                sb.Append("(@id, @name),");
                cmd.Parameters.AddWithValue("@id", owner.Entity.Id);
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
