using System.ComponentModel.DataAnnotations;

namespace Playgrounds.EfCore.Models.Database.Base;

public abstract class NamedEntityDb : BaseEntityDb
{
    [MaxLength(100)]
    public required string Name { get; set; } = null!;

    protected NamedEntityDb()
    {
        
    }

    protected NamedEntityDb(string name)
    {
        Name = name;
    }
}