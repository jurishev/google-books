using System.Text.Json;

namespace GoogleBooks.Models;

public class Volume
{
    public string Id { get; init; }
    public string SelfLink { get; init; }
    public VolumeInfo VolumeInfo { get; init; }

    public VolumeDto GetDto() => new()
    {
        Id = this.Id,
        SelfLink = this.SelfLink,
        Title = this.VolumeInfo.Title,
        Authors = JsonSerializer.Serialize(this.VolumeInfo.Authors),
        Publisher = this.VolumeInfo.Publisher,
        PublishedDate = this.VolumeInfo.PublishedDate,
        Description = this.VolumeInfo.Description,
        Categories = JsonSerializer.Serialize(this.VolumeInfo.Categories),
        PreviewLink = this.VolumeInfo.PreviewLink,
        SmallThumbnail = this.VolumeInfo.ImageLinks?.SmallThumbnail,
        Thumbnail = this.VolumeInfo.ImageLinks?.Thumbnail
    };
}

public class VolumeInfo
{
    public string Title { get; init; }
    public IEnumerable<string> Authors { get; init; }
    public string Publisher { get; init; }
    public string PublishedDate { get; init; }
    public string Description { get; init; }
    public IEnumerable<string> Categories { get; init; }
    public string PreviewLink { get; init; }
    public ImageLinks ImageLinks { get; init; }
}

public class ImageLinks
{
    public string SmallThumbnail { get; init; }
    public string Thumbnail { get; init; }
}

public class VolumeList
{
    public IEnumerable<Volume> Items { get; init; }
    public int TotalItems { get; init; }
}
