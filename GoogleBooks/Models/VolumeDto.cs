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
    public string PublishedDate { get; init; }
    public string Description { get; init; }
    public string IndustryIdentifiers { get; init; }
    public string Categories { get; init; }
    public string PreviewLink { get; init; }
    public string SmallThumbnail { get; init; }
    public string Thumbnail { get; init; }

    public Volume GetVolume() => new()
    {
        Id = this.Id,
        SelfLink = this.SelfLink,
        VolumeInfo = new VolumeInfo
        {
            Title = this.Title,
            Authors = JsonSerializer.Deserialize<IEnumerable<string>>(this.Authors),
            Publisher = this.Publisher,
            PublishedDate = this.PublishedDate,
            Description = this.Description,
            PreviewLink = this.PreviewLink,
            IndustryIdentifiers = JsonSerializer.Deserialize<IEnumerable<IndustryIdentifier>>(this.IndustryIdentifiers),
            Categories = JsonSerializer.Deserialize<IEnumerable<string>>(this.Categories),
            ImageLinks = new ImageLinks
            {
                SmallThumbnail = this.SmallThumbnail,
                Thumbnail = this.Thumbnail
            }
        }
    };
}
