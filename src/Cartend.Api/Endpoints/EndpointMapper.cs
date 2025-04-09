using Cartend.Api.Endpoints.Cars;
using Cartend.Api.Endpoints.Owners;

namespace Cartend.Api.Endpoints;

public static class EndpointMapper
{
    public static void MapAll(WebApplication app)
    {
        var owners = app.MapGroup("owners/");
        GetOwners.Map(owners);
        CreateOwner.Map(owners);

        var cars = app.MapGroup("cars/");
        GetOwnerCars.Map(cars);
        CreateCar.Map(cars);

        var tests = app.MapGroup("tests/");
        TestTransaction.Map(tests);
    }
}
