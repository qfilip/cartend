using Cartend.DataAccess.Entities;
using System.Text.Json.Nodes;

namespace Cartend.Dtos.V1.Entities;

public class CarServiceDto
{
    public Guid Id { get; set; }
    public string? ServicedBy { get; set; }
    public DateTime ServicedAt { get; set; }
    public JsonObject? WorkDone { get; set; }
    public CarDto? Car { get; set; }

    public static CarServiceDto From(CarService x)
    {
        return new()
        {
            Id = x.Entity.Id,
            ServicedAt = x.Entity.ServicedAt,
            ServicedBy = x.Entity.ServicedBy,
            WorkDone = x.Entity.WorkDone
        };
    }
}
