using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Models;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookshelvesController : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetBookshelfList(string userId)
    {
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(
            $"https://www.googleapis.com/books/v1/users/{userId}/bookshelves");

        return result.IsSuccessStatusCode ?
            this.Ok((await result.Content.ReadFromJsonAsync<BookshelfList>()).Items) :
            this.BadRequest(await result.Content.ReadAsStringAsync());
    }

    [HttpGet("{shelf}/user/{userId}")]
    public async Task<ActionResult> GetBookshelf(int shelf, string userId)
    {
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(
            $"https://www.googleapis.com/books/v1/users/{userId}/bookshelves/{shelf}");

        return result.IsSuccessStatusCode ?
            this.Ok(await result.Content.ReadFromJsonAsync<Bookshelf>()) :
            this.BadRequest(await result.Content.ReadAsStringAsync());
    }
}
