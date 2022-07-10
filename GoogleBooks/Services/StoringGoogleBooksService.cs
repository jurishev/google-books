using GoogleBooks.Models;
using GoogleBooks.Storage;

namespace GoogleBooks.Services;

public class StoringGoogleBooksService : IGoogleBooksService
{
    private readonly IGoogleBooksStorage storage;
    private readonly HttpClient httpClient;

    public StoringGoogleBooksService(IGoogleBooksStorage storage, HttpClient httpClient)
    {
        this.storage = storage;
        this.httpClient = httpClient;
    }

    public async Task<Bookshelf> GetBookshelf(int shelf, string userId)
    {
        var bookshelf = this.storage.GetBookshelf(shelf, userId);
        if (bookshelf is null)
        {
            bookshelf = await this.CallGoogleApi<Bookshelf>($"https://www.googleapis.com/books/v1/users/{userId}/bookshelves/{shelf}");
            if (bookshelf != null)
            {
                this.storage.AddBookshelf(bookshelf, userId);
            }
        }

        if (bookshelf != null)
        {
            bookshelf.Volumes = (await this.CallGoogleApi<VolumeList>($"https://www.googleapis.com/books/v1/users/{userId}/bookshelves/{shelf}/volumes"))?.Items;
        }

        return bookshelf;
    }

    public async Task<IEnumerable<Bookshelf>> GetBookshefList(string userId)
    {
        var bookshelves = (await this.CallGoogleApi<BookshelfList>($"https://www.googleapis.com/books/v1/users/{userId}/bookshelves"))?.Items;
        if (bookshelves != null)
        {
            foreach (var bookshelf in bookshelves)
            {
                this.storage.AddBookshelf(bookshelf, userId);
            }
        }

        return bookshelves;
    }

    public async Task<Volume> GetVolume(string volumeId)
    {
        var volume = this.storage.GetVolume(volumeId);
        if (volume is null)
        {
            volume = await this.CallGoogleApi<Volume>($"https://www.googleapis.com/books/v1/volumes/{volumeId}");
            if (volume != null)
            {
                this.storage.AddVolume(volume);
            }
        }

        return volume;
    }

    public async Task<IEnumerable<Volume>> GetVolumeList(VolumeQuery query)
    {
        var volumes = this.storage.GetVolumeList(query);
        if (volumes is null)
        {
            var terms = GetTerms(query);
            volumes = (await this.CallGoogleApi<VolumeList>($"https://www.googleapis.com/books/v1/volumes?q={terms}"))?.Items;
            if (volumes != null)
            {
                foreach (var volume in volumes)
                {
                    this.storage.AddVolume(volume);
                }
            }
        }

        return volumes;
    }

    private async Task<T> CallGoogleApi<T>(string uri)
    {
        var result = await this.httpClient.GetAsync(uri);
        return result.IsSuccessStatusCode ? await result.Content.ReadFromJsonAsync<T>() : default;
    }

    private static string GetTerms(VolumeQuery query)
    {
        var terms = new List<string>();

        AddTerm("intitle", query.Title, terms);
        AddTerm("inauthor", query.Author, terms);
        AddTerm("inpublisher", query.Publisher, terms);
        AddTerm("subject", query.Subject, terms);
        AddTerm("isbn", query.Isbn, terms);

        return string.Join('+', terms);
    }

    private static void AddTerm(string key, string value, List<string> terms)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            terms.Add($"{key}:{value}");
        }
    }
}
