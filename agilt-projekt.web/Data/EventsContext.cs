
using agilt_projekt.web.Models;
using Microsoft.EntityFrameworkCore;

namespace agilt_projekt.web.Data;

    public class EventsContext: DbContext
    {
        public DbSet<Event> UserEvents => Set<Event>();
        public EventsContext(DbContextOptions options) : base(options)
        {
        }

    }
