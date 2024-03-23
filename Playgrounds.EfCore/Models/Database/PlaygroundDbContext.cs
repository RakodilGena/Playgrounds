using Microsoft.EntityFrameworkCore;
using Playgrounds.EfCore.Models.Database.Authors;
using Playgrounds.EfCore.Models.Database.Posts;
using Playgrounds.EfCore.Models.Database.Tags;

namespace Playgrounds.EfCore.Models.Database;

public sealed class PlaygroundDbContext : DbContext
{
    public DbSet<AuthorDb> Authors { get; set; } = null!;

    public DbSet<PostDb> Posts { get; set; } = null!;

    public DbSet<TagDb> Tags { get; set; } = null!;

    public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<PostDb>().OwnsMany(
            post => post.Properties, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                //ownedNavigationBuilder.OwnsOne(property => property.SOME_OTHER_EMBEDDED_OWNED_TYPE);
            });
        
        modelBuilder.Entity<PostDb>()
            .HasIndex(post => post.Properties.Select(prop => prop.Key));
        
        base.OnModelCreating(modelBuilder);
    }
}