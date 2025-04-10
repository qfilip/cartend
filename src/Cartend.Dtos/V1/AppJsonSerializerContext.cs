using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Cartend.Dtos.V1.Entities;

[JsonSerializable(typeof(OwnerDto))]
[JsonSerializable(typeof(OwnerDto[]))]

[JsonSerializable(typeof(CarDto))]
[JsonSerializable(typeof(CarDto[]))]

[JsonSerializable(typeof(CarServiceDto))]
[JsonSerializable(typeof(CarServiceDto[]))]

[JsonSerializable(typeof(CreateOwnerRequest))]
[JsonSerializable(typeof(CreateCarRequest))]

[JsonSerializable(typeof(CreateCarServiceRequest))]

[JsonSerializable(typeof(JsonObject))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
