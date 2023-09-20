using System.Runtime.Intrinsics.Arm;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data;


// Uppsättning för Databas
public class EventApiContext : DbContext
{

    // Skapa en tabell xxxx med egenskaper från modellen.
    public DbSet<EventModel> Events { get; set; }
    public DbSet<AttendentModel> Attendents { get; set; }
    public EventApiContext(DbContextOptions<EventApiContext> options) : base(options)
    {



    }

    /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventModel>()
        .HasMany(e => e.Attendents)
        .WithOne()
        .HasForeignKey(a => a.EventId);





    } */
}