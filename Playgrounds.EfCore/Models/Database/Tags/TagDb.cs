using Playgrounds.EfCore.Models.Database.Base;
using Playgrounds.EfCore.Models.Database.Posts;

namespace Playgrounds.EfCore.Models.Database.Tags;

public sealed class TagDb : NamedEntityDb
{
    //todo many to many
    public IList<PostDb> Posts { get; init; } = [];

    private TagDb()
    {

    }

    // public TagDb(string name) : base(name)
    // {
    // }
}