using System.ComponentModel.DataAnnotations;
using Playgrounds.EfCore.Models.Database.Authors;
using Playgrounds.EfCore.Models.Database.Base;
using Playgrounds.EfCore.Models.Database.Tags;

namespace Playgrounds.EfCore.Models.Database.Posts;

public sealed class PostDb : BaseEntityDb
{
    public long AuthorId { get; init; }
    public required AuthorDb Author { get; init; } = null!;

    //todo many to many
    public IList<TagDb> Tags { get; init; } = [];

    [MaxLength(5000)]
    public required string Text { get; set; } = null!;

    //todo owned type with index on key
    //maybe later some indexed string search on value.
    /// <summary>
    /// Post properties. OWNED TYPE stored as JSON.
    /// </summary>
    public IList<PostProperty> Properties { get; init; } = [];

    private PostDb()
    {

    }

    // public PostDb(
    //     string text,
    //     AuthorDb author,
    //     IList<TagDb>? tags = null,
    //     IList<PostProperty>? properties = null)
    //
    // {
    //     Text = text;
    //     Author = author;
    //
    //     if (tags is not null)
    //         Tags = tags;
    //
    //     if (properties is not null)
    //         Properties = properties;
    // }
}