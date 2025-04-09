using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Cars;

public static class GetOwnerCars
{
    internal static Task<IResult> Action(
        [FromQuery] Guid ownerId,
        [FromServices] Cartend.Logic.Handlers.V1.GetOwnerCars.Handler handler) =>
        Pipeline.Pipe(ownerId, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapGet("", Action);
}