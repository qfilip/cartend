using Cartend.Api.Dtos.Entities;
using System.Text.Json.Serialization;

namespace Cartend.Api.Dtos;

[JsonSerializable(typeof(OwnerDto))]
[JsonSerializable(typeof(OwnerDto[]))]

[JsonSerializable(typeof(CarDto))]
[JsonSerializable(typeof(CarDto[]))]

[JsonSerializable(typeof(CreateOwnerRequest))]
[JsonSerializable(typeof(CreateCarRequest))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
