using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public class EntityFrameworkStorage : IStorage
{
    private readonly GoogleBooksContext context;

    public EntityFrameworkStorage(GoogleBooksContext context) => this.context = context;

    public Bookshelf GetBookshelf(int shelf, string userId) =>
        this.GetBookshelves(userId)
            .Where(sh => sh.Id == shelf).FirstOrDefault();

    public IEnumerable<Bookshelf> GetBookshelves(string userId) =>
        this.context.Bookshelves
            .Where(sh => sh.UserId == userId);

    public Volume GetVolume(string volumeId) =>
        this.context.Volumes
            .Where(v => v.Id == volumeId).FirstOrDefault()?.GetVolume();
}
