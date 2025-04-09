using Cartend.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints.Owners;

public static class CreateOwner
{
    internal static Task<IResult> Action(
        [FromBody] CreateOwnerRequest request,
        [FromServices] Logic.CreateOwner.Handler handler) =>
        Pipeline.Pipe(request, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapPost("", Action);
}
