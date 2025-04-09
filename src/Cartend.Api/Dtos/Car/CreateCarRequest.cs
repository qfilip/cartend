namespace Cartend.Api.Dtos;

public class CreateCarRequest
{
    public Guid OwnerId { get; set; }
    public string? Name { get; set; }
}
