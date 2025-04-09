using System.Text.Json.Serialization;

namespace Cartend.Dtos.V1.Entities;

[JsonSerializable(typeof(OwnerDto))]
[JsonSerializable(typeof(OwnerDto[]))]

[JsonSerializable(typeof(CarDto))]
[JsonSerializable(typeof(CarDto[]))]

[JsonSerializable(typeof(CreateOwnerRequest))]
[JsonSerializable(typeof(CreateCarRequest))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
