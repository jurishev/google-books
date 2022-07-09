using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Models;
using GoogleBooks.Services;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolumesController : ControllerBase
{
    private readonly IGoogleBooksService googleBooksService;

    public VolumesController(IGoogleBooksService service) => this.googleBooksService = service;

    [HttpGet]
    public async Task<ActionResult> GetVolumes([FromQuery] VolumeQuery query)
    {
        var volumes = await this.googleBooksService.GetVolumes(query);
        return volumes is null ? this.NotFound() : this.Ok(volumes);
    }

    [HttpGet("{volumeId}")]
    public async Task<ActionResult> GetVolume(string volumeId)
    {
        var volume = await this.googleBooksService.GetVolume(volumeId);
        return volume is null ? this.NotFound() : this.Ok(volume);
    }
}
