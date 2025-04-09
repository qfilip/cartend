using Cartend.Dtos.V1;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Owners;

public static class CreateOwner
{
    internal static Task<IResult> Action(
        [FromBody] CreateOwnerRequest request,
        [FromServices] Cartend.Logic.Handlers.V1.CreateOwner.Handler handler) =>
        Pipeline.Pipe(request, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapPost("", Action);
}
