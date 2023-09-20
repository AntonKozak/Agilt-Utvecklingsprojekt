using System.Runtime.Intrinsics.Arm;
using EventApi.Auth;
using EventApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data;


// Uppsättning för Databas med Identity
public class EventApiContext : IdentityDbContext<UserModel>
{

    // Skapa en tabell xxxx med egenskaper från modellen.
    public DbSet<EventModel> Events { get; set; }
    public DbSet<AttendentModel> Attendents { get; set; }
    public EventApiContext(DbContextOptions<EventApiContext> options) : base(options)
    {



    }
}