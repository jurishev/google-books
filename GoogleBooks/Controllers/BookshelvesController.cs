using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Models;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookshelvesController : ControllerBase
{
    [HttpGet("user/{userId}")]
    public IEnumerable<Bookshelf> Get(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{shelf}/user/{userId}")]
    public Bookshelf Get(int shelf, int userId)
    {
        throw new NotImplementedException();
    }
}
