using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints;

public class GetCarServices
{
    internal static Task<IResult> Action(
        [FromQuery] Guid carId,
        [FromServices] Cartend.Logic.Handlers.V1.GetCarServices.Handler handler) =>
        Pipeline.Pipe(carId, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapGet("", Action);
}
