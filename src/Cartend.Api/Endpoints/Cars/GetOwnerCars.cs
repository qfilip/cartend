using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Cars;

public static class GetOwnerCars
{
    internal static Task<IResult> Action(
        [FromQuery] Guid ownerId,
        [FromServices] Logic.GetOwnerCars.Handler handler) =>
        Pipeline.Pipe(ownerId, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapGet("", Action);
}