using Cartend.Api.DataAccess.Access;
using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Stores;

namespace Cartend.Api.DataAccess;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped(_ =>
        {
            var dbPath = Path.Combine(env.WebRootPath, "database.db3");
            var connectionString = $"Datasource={dbPath}";
            return new SqliteAccessor(connectionString);
        });

        services.AddScoped<OwnerStore>();
        services.AddScoped<CarStore>();

        services.AddScoped<AppDataStore>();
    }
}
