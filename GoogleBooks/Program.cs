using Microsoft.EntityFrameworkCore;
using GoogleBooks.Services;
using GoogleBooks.Storage;

namespace GoogleBooks;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services
            .AddScoped<IGoogleBooksStorage, EntityFrameworkGoogleBooksStorage>()
            .AddScoped<IGoogleBooksService, StoringGoogleBooksService>()
            .AddScoped<HttpClient>();

        builder.Services.AddDbContext<GoogleBooksContext>(
            options => options.UseInMemoryDatabase("GoogleBooks"));

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllers();

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
