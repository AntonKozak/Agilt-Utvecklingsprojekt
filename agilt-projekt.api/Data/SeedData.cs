using System.Text.Json;
using EventApi.Models;

namespace EventApi.Data;

public static class SeedData
{
    public static async Task LoadEvents(EventApiContext context){


        var options = new JsonSerializerOptions
        {

            // Den bryr sig inte om stora eller små bokstäver.
            PropertyNameCaseInsensitive = true
        };


        // Om datan redan är fylld, hoppa ur.
        if (context.Events.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/events.json");

        // Gör om formatet till en lista av typen EventModel
        var events = JsonSerializer.Deserialize<List<EventModel>>(json,options);


        // Kollar så att obejekten är över null/0.
        if (events is not null && events.Count > 0){

            //             Lägger till hela listan till events. Och sedan sprar det!.
            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();
        }
    }


       public static async Task LoadAttendents(EventApiContext context){


        var options = new JsonSerializerOptions
        {

            // Den bryr sig inte om stora eller små bokstäver.
            PropertyNameCaseInsensitive = true
        };


        // Om datan redan är fylld, hoppa ur.
        if (context.Attendents.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/Attendents.json");

        // Gör om formatet till en lista av typen AttendentModel
        var attendents = JsonSerializer.Deserialize<List<AttendentModel>>(json,options);


        // Kollar så att obejekten är över null/0.
        if (attendents is not null && attendents.Count > 0){

            //             Lägger till hela listan till personer som ska komma. Och sedan sprar det!.
            await context.Attendents.AddRangeAsync(attendents);
            await context.SaveChangesAsync();
        }
    }
}