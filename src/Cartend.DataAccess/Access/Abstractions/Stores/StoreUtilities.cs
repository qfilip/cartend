using Cartend.Api.DataAccess;
using Cartend.DataAccess.Abstractions;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Cartend.DataAccess.Access.Abstractions.Stores;

internal static class StoreUtilities
{
    internal static async Task<bool> EntityExistsAsync(Guid id, string tableName, IAccessor<SqliteCommand, SqliteDataReader> accessor)
    {
        var prep = (SqliteCommand cmd) =>
        {
            cmd.CommandText = $"SELECT Count(Id) as count FROM {tableName} WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
        };

        var read = (SqliteDataReader reader) =>
        {
            int count = 0;
            while (reader.Read())
                count = reader.GetFieldValue<int>("count");

            return count == 0;
        };

        return await accessor.QueryAsync(prep, read);
    }
}
