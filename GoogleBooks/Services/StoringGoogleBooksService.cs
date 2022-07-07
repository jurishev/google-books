using GoogleBooks.Models;
using GoogleBooks.Storage;

namespace GoogleBooks.Services
{
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

            return bookshelf;
        }

        public async Task<IEnumerable<Bookshelf>> GetBooksheves(string userId)
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

        public async Task<IEnumerable<Volume>> GetVolumes(string q)
        {
            var volumes = (await this.CallGoogleApi<VolumeList>($"https://www.googleapis.com/books/v1/volumes?q={q}"))?.Items;
            if (volumes != null)
            {
                foreach (var volume in volumes)
                {
                    this.storage.AddVolume(volume);
                }
            }

            return volumes;
        }

        private async Task<T> CallGoogleApi<T>(string uri)
        {
            var result = await this.httpClient.GetAsync(uri);
            return result.IsSuccessStatusCode ? await result.Content.ReadFromJsonAsync<T>() : default;
        }
    }
}
