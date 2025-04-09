using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Stores;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace Cartend.DataAccess;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services, string wwwrootPath)
    {
        services.AddScoped<IAccessor<SqliteCommand, SqliteDataReader>>(_ =>
        {
            var dbPath = Path.Combine(wwwrootPath, "database.db3");
            var connectionString = $"Datasource={dbPath}";
            return new SqliteAccessor(connectionString);
        });

        services.AddScoped<IOwnerStore, OwnerStore>();
        services.AddScoped<ICarStore, CarStore>();

        services.AddScoped<IAppDataStore, AppDataStore>();
    }
}
