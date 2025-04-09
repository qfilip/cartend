using Cartend.Api.DataAccess.Entities;

namespace Cartend.Api.Dtos.Entities;

public class OwnerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public CarDto[] Cars { get; set; } = Array.Empty<CarDto>();

    public static OwnerDto From(Owner x)
    {
        return new()
        {
            Id = x.Entity.Id,
            Name = x.Entity.Name,
            Cars = x.Relations.Cars.Select(CarDto.From).ToArray()
        };
    }
}