using Microsoft.EntityFrameworkCore;
using Npgsql;
using Playgrounds.EfCore.AppSettings;
using Playgrounds.EfCore.Models.Database;

namespace Playgrounds.EfCore;

public static class ServiceCollectionExtensions
{
    public static void AddPlaygroundsContext(this WebApplicationBuilder builder)
    {
        var connectionString = GetConnectionString(builder.Configuration);

        builder.Services.AddDbContext<PlaygroundDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static string GetConnectionString(this IConfiguration configuration)
    {
        var dbConnectionSection = configuration.GetSection("DbConnection");
        
        var opts = new DbConnectionOptions();
        dbConnectionSection.Bind(opts);
        
        var pgb = new NpgsqlConnectionStringBuilder
        {
            Host = opts.Host,
            Port = opts.Port,
            Database = opts.Database,
            Username = opts.Username,
            Password = opts.Password,
            CommandTimeout = opts.CommandTimeout
        };
        return pgb.ConnectionString;
    }
}