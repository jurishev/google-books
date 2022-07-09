using Microsoft.AspNetCore.Mvc;

namespace GoogleBooks.Models;

public class VolumeQuery
{
    public string Title { get; init; }
    public string Author { get; init; }
    public string Subject { get; init; }
    public string Publisher { get; init; }
    public string Isbn { get; init; }
}
