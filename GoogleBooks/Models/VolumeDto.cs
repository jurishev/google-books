using System.Text.Json;

namespace GoogleBooks.Models;

public class VolumeDto
{
    public string Id { get; init; }
    public string Etag { get; init; }
    public string SelfLink { get; init; }
    public string Title { get; init; }
    public string Authors { get; init; }
    public string Publisher { get; init; }
    public DateTime PublishedDate { get; init; }
    public string Description { get; init; }
    public string SmallThumbnail { get; init; }
    public string Thumbnail { get; init; }

    public Volume GetVolume() => new()
    {
        Id = this.Id,
        Etag = this.Etag,
        SelfLink = this.SelfLink,
        VolumeInfo = new VolumeInfo
        {
            Title = this.Title,
            Authors = JsonSerializer.Deserialize<IEnumerable<string>>(this.Authors),
            Publisher = this.Publisher,
            PublishedDate = this.PublishedDate,
            Description = this.Description,
            ImageLinks = new ImageLinks
            {
                SmallThumbnail = this.SmallThumbnail,
                Thumbnail = this.Thumbnail
            }
        }
    };
}
