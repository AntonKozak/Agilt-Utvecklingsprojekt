using System.Text.Json;
using System.Text.Json.Serialization;
using agilt_projekt.web.Models;

namespace agilt_projekt.web.Data;

    //Read json file and return list of objects
    public static class SeedData
    {
        public static async Task LoadEventData(EventsContext context){
            // take care A a F s beetwen Class and json
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
                // if DB UserEvents empty upload from json
            if(context.UserEvents.Any()) return;
                // REad json data
            var json = System.IO.File.ReadAllText("Data/json/events.json");
                // Transform json object to list off Events objects¨
            var listEvents = JsonSerializer.Deserialize<List<Event>>(json, options);

            if(listEvents is not null && listEvents.Count > 0) {
                // save to DB
                await context.UserEvents.AddRangeAsync(listEvents);
                await context.SaveChangesAsync();
            }
            // efter Program.cs efter build app
            //scope = app.Services.CreateScope
        }
    }
