using Cartend.Api.DataAccess.Access.Abstractions;
using Microsoft.Data.Sqlite;

namespace Cartend.Api.DataAccess.Access;

public class SqliteAccessor : IAccessor<SqliteCommand, SqliteDataReader>
{
    private readonly string _connectionString;
    private List<Action<SqliteCommand>> _transactionCommandsPrep = new();
    public SqliteAccessor(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddTransactionCommandPrep(Action<SqliteCommand> prep) => _transactionCommandsPrep.Add(prep);

    public async Task<T> QueryAsync<T>(
        Action<SqliteCommand> prepareCommand,
        Func<SqliteDataReader, T> readData)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();
        prepareCommand(command);
        try
        {
            connection.Open();
            var reader = await command.ExecuteReaderAsync();
            var result = readData(reader);
            connection.Close();

            return result;
        }
        catch (Exception _)
        {
            Console.WriteLine("Dang");
            throw;
        }
    }

    public async Task<int> CommandAsync(Action<SqliteCommand> prepareCommand)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();
        prepareCommand(command);
        try
        {
            connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            connection.Close();

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Dang");
            throw;
        }
    }

    public async Task<int> CommitAsync()
    {
        var result = 0;
        if (_transactionCommandsPrep.Count == 0) return result;

        using var connection = new SqliteConnection(_connectionString);
        

        connection.Open();
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

            connection.Close();
            _transactionCommandsPrep.Clear();
        }

        return result;
    }
}
