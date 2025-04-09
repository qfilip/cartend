using Cartend.Api.Logic.Abstractions;

namespace Cartend.Api.Logic;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        // owners
        services.AddTransient<GetAllOwners.Handler>();
        services.AddTransient<CreateOwner.Handler>();

        // cars
        services.AddTransient<GetOwnerCars.Handler>();
        services.AddTransient<CreateCar.Handler>();

        // tests
        services.AddTransient<TestTransaction.Handler>();
    }
}
