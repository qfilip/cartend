using Cartend.Api.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints;

public static class GetOwners
{
    internal static Task<IResult> Action([FromServices] Logic.GetAllOwners.Handler handler) =>
        Pipeline.Pipe(Unit.New, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapGet("", Action);
}
