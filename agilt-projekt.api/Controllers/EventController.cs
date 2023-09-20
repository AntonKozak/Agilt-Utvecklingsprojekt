using Microsoft.AspNetCore.Mvc;

namespace EventApi.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{

    // Retunera lista av events
    [HttpGet()]
    public async Task<ActionResult> GetAllEvents()
    {

        return StatusCode(200, new { message = "Event" });
    }

    [HttpPost]
    public async Task<ActionResult> CreateEvent()
    {
        return StatusCode(201, new { message = "Event created" });
    }

    [HttpPut]

}