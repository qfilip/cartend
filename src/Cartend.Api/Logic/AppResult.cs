namespace Cartend.Api.Logic;

public class AppResult
{
    private AppResult(eOpcode status, string[] errors, object? result)
    {
        Status = status;
        Errors = errors;
        Object = result;
    }
    public static AppResult Ok() => new AppResult(eOpcode.Ok, [], null);
    public static AppResult Ok(object result) => new AppResult(eOpcode.Ok, [], result);
    public static AppResult NotFound() => new AppResult(eOpcode.NotFound, [], null);
    public static AppResult Rejected(string error) => new AppResult(eOpcode.Rejected, [error], null);
    public static AppResult Rejected(IEnumerable<string> errors) => new AppResult(eOpcode.Rejected, errors.ToArray(), null);

    public eOpcode Status { get; set; }
    public string[] Errors { get; set; }
    public object? Object { get; set; }
    public enum eOpcode { Ok, NotFound, Rejected }
}
