using Cartend.Api.DataAccess.Abstractions;

namespace Cartend.Api.DataAccess.Tables;

public class CarTable : IPkey<Guid>
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string? Name { get; set; }
}
