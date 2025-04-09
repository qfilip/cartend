using Cartend.Api.Logic;

namespace Cartend.Logic.Abstractions;

public interface IAppHandler<TRequest>
{
    Task<AppResult> Handle(TRequest request);
    string[] Validate(TRequest request);
}
