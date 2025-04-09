using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Access;
using Microsoft.Data.Sqlite;

namespace Cartend.DataAccess;

public class SqliteAccessor : IAccessor<SqliteCommand, SqliteDataReader>
{
    private readonly ConnectionPool _pool;
    private List<Action<SqliteCommand>> _transactionCommandsPrep = new();
    public SqliteAccessor(ConnectionPool pool)
    {
        _pool = pool;
    }

    public void AddTransactionCommandPrep(Action<SqliteCommand> prep) => _transactionCommandsPrep.Add(prep);

    public async Task<T> QueryAsync<T>(
        Action<SqliteCommand> prepareCommand,
        Func<SqliteDataReader, T> readData)
    {
        var connection = await _pool.GetConnection();
        using var command = connection.CreateCommand();
        prepareCommand(command);
        try
        {
            var reader = await command.ExecuteReaderAsync();
            var result = readData(reader);

            return result;
        }
        catch (Exception _)
        {
            Console.WriteLine("Dang");
            throw;
        }
        finally
        {
            _pool.ReleaseConnection();
        }
    }

    public async Task<int> CommandAsync(Action<SqliteCommand> prepareCommand)
    {
        var connection = await _pool.GetConnection();
        using var command = connection.CreateCommand();
        prepareCommand(command);
        try
        {
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Dang");
            throw;
        }
        finally
        {
            _pool.ReleaseConnection();
        }
    }

    public async Task<int> CommitAsync()
    {
        var result = 0;
        if (_transactionCommandsPrep.Count == 0) return result;

        var connection = await _pool.GetConnection();
        using var transaction = connection.BeginTransaction();
        
        var commands = _transactionCommandsPrep
            .Select(prepFn =>
            {
                var command = connection.CreateCommand();
                command.Transaction = transaction;
                prepFn(command);
                
                return command;
            }).ToArray();
        try
        {
            foreach (var cmd in commands)
                result += await cmd.ExecuteNonQueryAsync();

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            foreach (var cmd in commands)
                cmd.Dispose();

            _transactionCommandsPrep.Clear();
            _pool.ReleaseConnection();
        }

        return result;
    }
}
