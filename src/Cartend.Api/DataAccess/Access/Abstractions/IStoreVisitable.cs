namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface IStoreVisitable
{
    void Accept(SqliteAccessor dataAccessor);
}
