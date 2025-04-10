using System.Text.Json.Nodes;

namespace Cartend.Dtos.V1;

public class CreateCarServiceRequest
{
    public Guid CarId { get; set; }
    public string? ServicedBy { get; set; }
    public JsonObject? WorkDone { get; set; }
}
