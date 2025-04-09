using Cartend.Api.DataAccess.Abstractions;
using System.Text.Json.Nodes;

namespace Cartend.Api.DataAccess.Tables;

public class CarService : IPkey<Guid>
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public string? ServicedBy { get; set; }
    public DateTime ServicedAt { get; set; }
    public JsonObject? WorkDone { get; set; }
}
