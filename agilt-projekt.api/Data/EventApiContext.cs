using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data;


// Uppsättning för Databas
public class EventApiContext : DbContext
{

    // Skapa en tabell xxxx med egenskaper från modellen.
    public DbSet<EventModel> Events => Set<EventModel>();
    public DbSet<AttendentModel> Attendents => Set<AttendentModel>();
    public EventApiContext(DbContextOptions options) : base(options)
    {



    }
}