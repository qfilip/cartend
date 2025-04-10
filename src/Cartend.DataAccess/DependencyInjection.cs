using Cartend.DataAccess.Abstractions;
using Cartend.DataAccess.Access;
using Cartend.DataAccess.Stores;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace Cartend.DataAccess;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services, string wwwrootPath)
    {
        services.AddSingleton(_ =>
        {
            var dbPath = Path.Combine(wwwrootPath, "database.db3");
            var connectionString = $"Datasource={dbPath}";
            
            return new ConnectionPool(connectionString);
        });

        services.AddScoped<IAccessor<SqliteCommand, SqliteDataReader>>(sp =>
        {
            var connectionPool = sp.GetRequiredService<ConnectionPool>();
            if (connectionPool == null)
                throw new InvalidOperationException($"{nameof(ConnectionPool)} service not found.");
            
            return new SqliteAccessor(connectionPool);
        });

        services.AddScoped<IOwnerStore, OwnerStore>();
        services.AddScoped<ICarStore, CarStore>();
        services.AddScoped<ICarServiceStore, CarServiceStore>();

        services.AddScoped<IAppDataStore, AppDataStore>();
    }
}
