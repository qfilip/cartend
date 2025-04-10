namespace Cartend.Api.DataAccess;

public static class Validators
{
    public static class Owner
    {
        public static class Name
        {
            public static string? Validate(string? x) => x != null && x.Length > 1 ? null : "Name must have at least 2 chars";
        }
    }

    public static class Car
    {
        public static class Name
        {
            public static string? Validate(string? x) => x != null && x.Length > 1 ? null : "Name must have at least 2 chars";
        }
    }

    public static class CarService
    {
        public static class ServicedBy
        {
            public static string? Validate(string? x) => x != null && x.Length > 1 ? null : "ServicedBy must have at least 2 chars";
        }
    }
}
