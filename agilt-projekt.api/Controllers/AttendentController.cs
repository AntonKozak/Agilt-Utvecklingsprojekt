namespace EventApi.Controllers;

using agilt_projekt.api.Data.Migrations;
using EventApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class AttedentController : ControllerBase
{

    private readonly EventApiContext _context;


    public AttedentController(EventApiContext context)
    {
        _context = context;

    }

    [HttpGet()]

    public async Task<IActionResult> GetAllAttendents(){
            var result = await _context.Attendents.ToListAsync();

            return Ok(result);
    }


    [HttpPost("{EventId}/{AttendentId}")]

    public async Task<IActionResult> AddAttendentToEvent(int EventId, int AttendentId){

        var CurrentEvent = await _context.Events.FindAsync(EventId);

        if(CurrentEvent is null) return NotFound($"Eventet med id: {EventId} finns ej");
        
        var attendent = await _context.Attendents.FindAsync(AttendentId);
        if(attendent is null) return NotFound($"Den personen finns inte, registrera dig först!");


        var AttendentToAdd = await _context.Events
        .Include(e => e.Attendents)
        .FirstOrDefaultAsync(e => e.EventId == EventId);

        CurrentEvent.Attendents.Add(AttendentToAdd);

        




        return StatusCode(500);
    }




}

