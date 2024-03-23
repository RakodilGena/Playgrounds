using System.ComponentModel.DataAnnotations;
using Playgrounds.EfCore.Models.Database.Addresses;
using Playgrounds.EfCore.Models.Database.Base;
using Playgrounds.EfCore.Models.Database.Posts;

namespace Playgrounds.EfCore.Models.Database.Authors;

public sealed class AuthorDb : NamedEntityDb
{
    public IList<PostDb> Posts { get; init; } = [];
    
    [MaxLength(500)]
    public IList<string> MetaData { get; init; } = [];
    
    /// <summary>
    /// Address of author. COMPLEX TYPE.
    /// </summary>
    public required Address Address { get; set; }

    private AuthorDb()
    {
    }

    // public AuthorDb(string name) : base(name)
    // {
    //     
    // }
}