using Microsoft.EntityFrameworkCore;
using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public class GoogleBooksContext : DbContext
{
    public GoogleBooksContext(DbContextOptions<GoogleBooksContext> options)
        : base(options) => this.Database.EnsureCreated();

    public DbSet<Bookshelf> Bookshelves { get; set; }
    public DbSet<VolumeDto> Volumes { get; set; }
}
