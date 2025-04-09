namespace Cartend.Api.DataAccess.Access.Abstractions;

public interface IStoreVisitor
{
    void Visit(IStoreVisitable store);
}
