using Microsoft.Data.Sqlite;

namespace Cartend.DataAccess.Access;

public class ConnectionPool
{
    private SqliteConnection[] _connections;
    private SemaphoreSlim _requestGate;
    private SemaphoreSlim _connectionsGate;
    public ConnectionPool(string connectionString)
    {
        _connections = Enumerable.Range(0, 3)
            .Select(x => new SqliteConnection(connectionString))
            .ToArray();

        _connectionsGate = new(_connections.Length);
        _requestGate = new SemaphoreSlim(1);
    }

    public async Task<SqliteConnection> GetConnectionAsync()
    {
        await _requestGate.WaitAsync();
        await _connectionsGate.WaitAsync();
        
        var connection = _connections.First(x => x.State == System.Data.ConnectionState.Closed);
        connection.Open();
        _requestGate.Release();

        return connection;
    }

    public void ReleaseConnection(SqliteConnection connection)
    {
        connection.Close();
        _connectionsGate.Release();
    }
}
