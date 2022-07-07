using GoogleBooks.Models;

namespace GoogleBooks.Services;

public interface IGoogleBooksService
{
    Task<Bookshelf> GetBookshelf(int shelf, string userId);
    Task<IEnumerable<Bookshelf>> GetBooksheves(string userId);

    Task<Volume> GetVolume(string volumeId);
    Task<IEnumerable<Volume>> GetVolumes(string q);
}
