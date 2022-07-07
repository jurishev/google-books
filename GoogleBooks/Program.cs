using Microsoft.EntityFrameworkCore;
using GoogleBooks.Services;
using GoogleBooks.Storage;

namespace GoogleBooks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services
                .AddScoped<IGoogleBooksStorage, EntityFrameworkGoogleBooksStorage>()
                .AddScoped<IGoogleBooksService, StoringGoogleBooksService>()
                .AddScoped<HttpClient>();

            builder.Services.AddDbContext<GoogleBooksContext>(
                options => options.UseInMemoryDatabase("GoogleBooks"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}