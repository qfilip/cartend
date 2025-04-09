using Cartend.DataAccess.Abstractions;

namespace Cartend.DataAccess.Tables;

public class OwnerTable : IPkey<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
