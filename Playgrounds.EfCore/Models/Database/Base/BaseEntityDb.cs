using System.ComponentModel.DataAnnotations;

namespace Playgrounds.EfCore.Models.Database.Base;

public abstract class BaseEntityDb
{
    [Key]
    public long Id { get; init; }
}