using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Models;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolumesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Volume> Get([FromQuery] string q)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{volumeId}")]
    public Volume Get(int volumeId)
    {
        throw new NotImplementedException();
    }
}
