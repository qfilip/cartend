namespace Cartend.Api.DataAccess;

public static class Validators
{
    public static class Owner
    {
        public static class Name
        {
            public static bool Validate(string? x) => x != null && x.Length > 1;
            public static string Error => "Name must have at least 2 chars";
        }
    }

    public static class Car
    {
        public static class Name
        {
            public static bool Validate(string? x) => x != null && x.Length > 1;
            public static string Error => "Name must have at least 2 chars";
        }
    }
}
