namespace Cartend.DataAccess.Abstractions;

public interface IAccessor<TCommand, TReader>
{
    Task<int> CommandAsync(Action<TCommand> commandPrep);
    Task<T> QueryAsync<T>(Action<TCommand> commandPrep, Func<TReader, T> read);
    void AddTransactionCommandPrep(Action<TCommand> commandPrep);
    Task<int> CommitAsync();
}
