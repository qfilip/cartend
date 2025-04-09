using Cartend.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Cars;

public static class CreateCar
{
    internal static Task<IResult> Action(
        [FromBody] CreateCarRequest request,
        [FromServices] Logic.CreateCar.Handler handler) =>
        Pipeline.Pipe(request, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapPost("", Action);
}
