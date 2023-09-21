using agilt_projekt.api.ViewModel;
using EventApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EventApi.Models;

namespace EventApi.Controllers;



[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{

    private readonly EventApiContext _context;
    public EventController(EventApiContext context)
    {

        _context = context;

    }

    // Retunera lista av events samt vilka som är anmälda
    [HttpGet()]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetAllEvents()
    {

        var result = await _context.Events
        .Select(e => new
        {
            Id = e.EventId,
            Event = e.EventName,
            Anmälda = e.Attendents.Select(a => new
            {

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
        // bör man projicera till en viewmodel istället för anonymt objekt?
        var result = await _context.Events
        .Select(e => new
        {
            Id = e.EventId,
            Event = e.EventName,
            Anmälda = e.Attendents.Select(a => new
            {
                Namn = $"{a.FirstName} {a.LastName}",
                Telefon = a.PhoneNumber
            }).ToList(),
        })
        .SingleOrDefaultAsync(e => e.Id == eventId);
        return Ok(result);
    }

    // Skapa event
    [HttpPost()]
    public async Task<ActionResult> Create(EventPostViewModel model)
    {
        var e = new EventModel
        {
            EventName = model.EventName,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

        await _context.Events.AddAsync(e);

        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = e.EventId });
        }
        return StatusCode(500, "Internal Server Error");
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
