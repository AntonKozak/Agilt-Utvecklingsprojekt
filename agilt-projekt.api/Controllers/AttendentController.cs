namespace EventApi.Controllers;


using EventApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/Attedent")]
public class AttedentController : ControllerBase
{

    private readonly EventApiContext _context;


    public AttedentController(EventApiContext context)
    {
        _context = context;

    }

    [HttpGet()]

    public async Task<IActionResult> GetAllAttendents()
    {
        var result = await _context.Attendents.ToListAsync();

        return Ok(result);
    }


    [HttpPost("{EventId}/{AttendentId}")]
    public async Task<IActionResult> AddAttendentToEvent(int EventId, int AttendentId)
    {
        var currentEvent = await _context.Events
                                .Include(e => e.Attendents)
                                .FirstOrDefaultAsync(e => e.EventId == EventId);

        if (currentEvent == null)
            return NotFound($"Event with ID: {EventId} does not exist.");

        var attendant = await _context.Attendents.FindAsync(AttendentId);

        if (attendant == null)
            return NotFound($"The person does not exist, please register first.");

        currentEvent.Attendents.Add(attendant);

        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {

            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }





}

