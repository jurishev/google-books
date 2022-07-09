using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public interface IGoogleBooksStorage
{
    void AddBookshelf(Bookshelf bookshelf, string userId);
    void AddVolume(Volume volume);

    Bookshelf GetBookshelf(int shelf, string userId);
    Volume GetVolume(string volumeId);

    IEnumerable<Volume> GetVolumeList(VolumeQuery query);
}
