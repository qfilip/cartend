using Cartend.Dtos.V1;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Cars;

public static class CreateCar
{
    internal static Task<IResult> Action(
        [FromBody] CreateCarRequest request,
        [FromServices] Cartend.Logic.Handlers.V1.CreateCar.Handler handler) =>
        Pipeline.Pipe(request, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapPost("", Action);
}
