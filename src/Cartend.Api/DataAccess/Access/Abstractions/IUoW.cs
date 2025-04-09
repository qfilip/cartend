namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface IUoW
{
    Task<int> CommitAsync();
}
