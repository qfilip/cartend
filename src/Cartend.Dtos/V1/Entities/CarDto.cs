using Cartend.DataAccess.Entities;

namespace Cartend.Dtos.V1.Entities;

public class CarDto
{
    public Guid Id { get; set; }
    public OwnerDto? Owner { get; set; }
    public string? Name { get; set; }

    public static CarDto From(Car x)
    {
        return new CarDto
        {
            Id = x.Entity.Id,
            Name = x.Entity.Name,
            Owner = x.Relations.Owner != null ? OwnerDto.From(x.Relations.Owner) : null,
        };
    }
}
