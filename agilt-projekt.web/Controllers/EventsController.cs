using agilt_projekt.web.Data;
using agilt_projekt.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace agilt_projekt.web.Controllers;

    [Route("events")]
    public class EventsController : Controller
    {
        private readonly EventsContext _context;
    public EventsController(EventsContext context)
    {
        _context = context;

    }

        //TASK make method awaited resultat the sane IActionResult(result HTTP req.)
    public async Task<IActionResult> Index()
        {
            // UserEvents its DB. We are reading data from DB and making List with objects
            var eventsList = await _context.UserEvents.ToListAsync();
            // var events = new List<Event>{
            //     new(){EventId = 1, Name = "Study C#", Description = "Learning C# every day"},
            //     new(){EventId = 2, Name = "Meeting in Zoom", Description = "We shoud speak about LIA"},
            //     new(){EventId = 3, Name = "JavaScript", Description = "Saturday meeting to explaine JavaScript"},
            //     new(){EventId = 4, Name = "Get Post Put", Description = "Explanation request and answer"},
            // };
            // return View("Index", events);
            return View("Index", eventsList);
        }
    }
