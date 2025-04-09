using Microsoft.Data.Sqlite;

namespace Cartend.DataAccess.Access;

public class ConnectionPool
{
    private SqliteConnection _connection;
    private SemaphoreSlim Gate = new(1);
    public ConnectionPool(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
    }

    public async Task<SqliteConnection> GetConnection()
    {
        await Gate.WaitAsync();
        _connection.Open();
        return _connection;
    }

    public void ReleaseConnection()
    {
        _connection.Close();
        Gate.Release();
    }
}
