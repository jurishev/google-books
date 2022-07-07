using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public class EntityFrameworkGoogleBooksStorage : IGoogleBooksStorage
{
    private readonly GoogleBooksContext context;

    public EntityFrameworkGoogleBooksStorage(GoogleBooksContext context) => this.context = context;

    public void AddBookshelf(Bookshelf bookshelf, string userId)
    {
        var storedBookshelf = this.GetBookshelf(bookshelf.Id, userId);
        if (storedBookshelf is null)
        {
            bookshelf.UserId = userId;
            this.context.Bookshelves.Add(bookshelf);
            this.context.SaveChanges();
        }
    }

    public void AddVolume(Volume volume)
    {
        var storedVolume = this.GetVolume(volume.Id);
        if (storedVolume is null)
        {
            this.context.Volumes.Add(volume.GetDto());
            this.context.SaveChanges();
        }
    }

    public Bookshelf GetBookshelf(int shelf, string userId) => this.context.Bookshelves.Find(userId, shelf);

    public Volume GetVolume(string volumeId) => this.context.Volumes.Find(volumeId)?.GetVolume();
}
