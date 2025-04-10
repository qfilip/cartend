using Cartend.Dtos.V1;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints;

public static class CreateCarService
{
    internal static Task<IResult> Action(
    [FromBody] CreateCarServiceRequest request,
    [FromServices] Cartend.Logic.Handlers.V1.CreateCarService.Handler handler) =>
    Pipeline.Pipe(request, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapPost("", Action);
}
