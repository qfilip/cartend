using Cartend.Api.Logic;
using Cartend.Api.Logic.Abstractions;
using static Cartend.Api.Logic.AppResult;

namespace Cartend.Api.Endpoints;

public static class Pipeline
{
    public static async Task<IResult> Pipe<T>(
        T request,
        IAppHandler<T> handler,
        Func<AppResult, IResult>? customOkResultMapper = null)
    {
        var errors = handler.Validate(request);
        if (errors.Length > 0)
            return Results.BadRequest(errors);

        var result = await handler.Handle(request);

        return MapResult(result, customOkResultMapper);
    }

    private static IResult MapResult(
        AppResult handlerResult,
        Func<AppResult, IResult>? customOkResultMapper = null)
    {
        return (handlerResult.Status, customOkResultMapper) switch
        {
            (eOpcode.Ok, null) => Results.Ok(handlerResult.Object),
            (eOpcode.Ok, _) => customOkResultMapper.Invoke(handlerResult),
            (eOpcode.NotFound, _) => Results.NotFound(handlerResult.Object),
            (eOpcode.Rejected, _) => Results.Conflict(handlerResult.Errors),
            _ =>
                throw new NotImplementedException($"Cannot handle status of {handlerResult.Status}")
        };
    }
}
