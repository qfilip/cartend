using Cartend.Api.DataAccess.Access;
using Cartend.Api.DataAccess.Access.Abstractions;
using Cartend.Api.DataAccess.Access.Abstractions.Stores;
using Cartend.Api.DataAccess.Access.Stores;
using Microsoft.Data.Sqlite;

namespace Cartend.Api.DataAccess;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IAccessor<SqliteCommand, SqliteDataReader>>(_ =>
        {
            var dbPath = Path.Combine(env.WebRootPath, "database.db3");
            var connectionString = $"Datasource={dbPath}";
            return new SqliteAccessor(connectionString);
        });

        services.AddScoped<IOwnerStore, OwnerStore>();
        services.AddScoped<ICarStore, CarStore>();

        services.AddScoped<IAppDataStore, AppDataStore>();
    }
}
