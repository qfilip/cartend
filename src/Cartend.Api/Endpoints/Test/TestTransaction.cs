using Microsoft.AspNetCore.Mvc;

namespace Cartend.Api.Endpoints;

public static class TestTransaction
{
    internal static Task<IResult> Action(
        [FromQuery] bool fail,
        [FromServices] Cartend.Logic.Handlers.V1.TestTransaction.Handler handler) =>
        Pipeline.Pipe(fail, handler);

    internal static void Map(RouteGroupBuilder builder) => builder.MapGet("transaction", Action);
}
