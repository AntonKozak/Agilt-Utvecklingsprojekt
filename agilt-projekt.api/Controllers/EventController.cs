using EventApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{

    private readonly EventApiContext _context;
    public EventController(EventApiContext context){

        _context = context;

    }

    // Retunera lista av events samt vilka som är anmälda
    [HttpGet()]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetAllEvents()
    {

       var result = await _context.Events
       .Select(e => new {
        Id = e.EventId,
        Event = e.EventName,
        Anmälda = e.Attendents.Select(a => new {

            Namn = $"{a.FirstName} {a.LastName}",
            Telefon = a.PhoneNumber


        }).ToList(),

       }).ToListAsync();

       return Ok(result);

    }

    // Hämta event på id
    [HttpGet("{eventId}")]
    public async Task<ActionResult> GetById(int eventId)
    {
        return Ok();
    }

    // Skapa event
    [HttpPost()]
    public async Task<ActionResult> Create()
    {
        return StatusCode(201, new { message = "Event created" });
    }

    // Uppdatera event
    [HttpPut("{eventId}")]
    public async Task<ActionResult> Update(int eventId)
    {
        return NoContent();
    }

    // Ta bort event
    [HttpDelete("eventId")]
    public async Task<ActionResult> Delete(int eventId)
    {
        return NoContent();
    }

}