namespace Cartend.Dtos.V1;

public class CreateCarRequest
{
    public Guid OwnerId { get; set; }
    public string? Name { get; set; }
}
