using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Playgrounds.EfCore.Models.Database;

namespace Playgrounds.EfCore.DesignTimeContextFactory;

[UsedImplicitly]
public sealed class PlaygroundDbContextFactory:  IDesignTimeDbContextFactory<PlaygroundDbContext>
{
    public PlaygroundDbContext CreateDbContext(string[] args)
    {
        string dir = Directory.GetCurrentDirectory();
        
        string path = string.Concat(dir, @"\appsettings.json");
        
        var config = 
            new ConfigurationBuilder().SetBasePath(dir)
                .AddJsonFile(path, optional: false, reloadOnChange: false).Build();

        var connectionString = config.GetConnectionString();
        Console.WriteLine(connectionString);
        
        var optionsBuilder = new DbContextOptionsBuilder<PlaygroundDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PlaygroundDbContext(optionsBuilder.Options);
    }
}