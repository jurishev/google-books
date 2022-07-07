using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GoogleBooks.Models;

public class Bookshelf
{
    [Key]
    [JsonIgnore]
    public string StorageId { get; init; }

    [JsonIgnore]
    public string UserId { get; init; }

    public int Id { get; init; }
    public string Title { get; init; }
    public string Access { get; init; }
    public DateTime Updated { get; init; }
    public DateTime Created { get; init; }
    public int VolumeCount { get; init; }
    public DateTime VolumesLastUpdated { get; init; }
}

public class BookshelfList
{
    public IEnumerable<Bookshelf> Items { get; init; }
}
