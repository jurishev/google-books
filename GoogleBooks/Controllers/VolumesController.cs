using Microsoft.AspNetCore.Mvc;
using GoogleBooks.Models;

namespace GoogleBooks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolumesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetVolumeList([FromQuery] string q)
    {
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(
            $"https://www.googleapis.com/books/v1/volumes?q={q}");

        return result.IsSuccessStatusCode ?
            this.Ok((await result.Content.ReadFromJsonAsync<VolumeList>()).Items) :
            this.BadRequest(await result.Content.ReadAsStringAsync());
    }

    [HttpGet("{volumeId}")]
    public async Task<ActionResult> GetVolume(string volumeId)
    {
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(
            $"https://www.googleapis.com/books/v1/volumes/{volumeId}");

        return result.IsSuccessStatusCode ?
            this.Ok(await result.Content.ReadFromJsonAsync<Volume>()) :
            this.BadRequest(await result.Content.ReadAsStringAsync());
    }
}
