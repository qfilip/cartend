using Cartend.Api.DataAccess.Abstractions;

namespace Cartend.Api.DataAccess.Tables;

public class OwnerTable : IPkey<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
