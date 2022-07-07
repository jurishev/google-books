using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public interface IStorage
{
    Bookshelf GetBookshelf(int shelf, string userId);

    IEnumerable<Bookshelf> GetBookshelves(string userId);

    Volume GetVolume(string volumeId);
}
