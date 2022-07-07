using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Services;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookshelvesController : ControllerBase
{
    private readonly IGoogleBooksService googleBooksService;

    public BookshelvesController(IGoogleBooksService service) => this.googleBooksService = service;

    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetBookshelves(string userId)
    {
        var bookshelves = await this.googleBooksService.GetBooksheves(userId);
        return bookshelves is null ? this.NotFound() : this.Ok(bookshelves);
    }

    [HttpGet("{shelf}/user/{userId}")]
    public async Task<ActionResult> GetBookshelf(int shelf, string userId)
    {
        var bookshelf = await this.googleBooksService.GetBookshelf(shelf, userId);
        return bookshelf is null ? this.NotFound() : this.Ok(bookshelf);
    }
}
