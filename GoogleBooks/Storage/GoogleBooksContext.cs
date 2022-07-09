using Microsoft.EntityFrameworkCore;
using GoogleBooks.Models;

namespace GoogleBooks.Storage;

public class GoogleBooksContext : DbContext
{
    public GoogleBooksContext(DbContextOptions<GoogleBooksContext> options)
        : base(options) => this.Database.EnsureCreated();

    public DbSet<Bookshelf> Bookshelves { get; set; }
    public DbSet<VolumeDto> Volumes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Bookshelf>().HasKey(nameof(Bookshelf.UserId), nameof(Bookshelf.Id));
        builder.Entity<Bookshelf>().Ignore(b => b.Volumes);
    }
}
