namespace Cartend.Api.Logic;

public struct Unit
{
    private static Unit _instance = new();
    public static Unit New => _instance;
}
