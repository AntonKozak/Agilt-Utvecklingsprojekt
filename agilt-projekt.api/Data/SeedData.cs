using System.Text.Json;
using EventApi.Auth;
using EventApi.Models;
using Microsoft.AspNetCore.Identity;

namespace EventApi.Data;

public static class SeedData
{


    // Lägg till testRoller och användare
    // Skappa dummy användare och roller
    public static async Task LoadRolesAndUsers(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
    {

        // Om inga roller finns skappa dessa nedan
        if(!roleManager.Roles.Any()){
            var admin = new IdentityRole{Name="Admin", NormalizedName="ADMIN"};
            var user = new IdentityRole{Name="User", NormalizedName="USER"};

            await roleManager.CreateAsync(admin);
            await roleManager.CreateAsync(user);
        }

        // Om inga användare finns skapa nya!
        if(!userManager.Users.Any()){
            var admin = new UserModel{
                UserName = "badmondy@gmail.com",
                Email = "badmondy@gmail.com",
                FirstName = "Oskar",
                LastName = "Markbäck Zeilon"
            };



            await userManager.CreateAsync(admin,"Pa$$W0rd");

            await userManager.AddToRolesAsync(admin, new[]{"Admin","User"});


            var user = new UserModel{
                UserName = "maria@gmail.com",
                Email = "maria@gmail.com",
                FirstName = "Maria",
                LastName = "Hedqvist",

            };

            await userManager.CreateAsync(user,"Pas$$W0rd");

            await userManager.AddToRoleAsync(user,"User");
        }
    }

    public static async Task LoadEvents(EventApiContext context)
    {


        var options = new JsonSerializerOptions
        {

            // Den bryr sig inte om stora eller små bokstäver.
            PropertyNameCaseInsensitive = true
        };


        // Om datan redan är fylld, hoppa ur.
        if (context.Events.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/events.json");

        // Gör om formatet till en lista av typen EventModel
        var events = JsonSerializer.Deserialize<List<EventModel>>(json, options);


        // Kollar så att obejekten är över null/0.
        if (events is not null && events.Count > 0)
        {

            //             Lägger till hela listan till events. Och sedan sprar det!.
            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();
        }
    }


    public static async Task LoadAttendents(EventApiContext context)
    {


        var options = new JsonSerializerOptions
        {

            // Den bryr sig inte om stora eller små bokstäver.
            PropertyNameCaseInsensitive = true
        };


        // Om datan redan är fylld, hoppa ur.
        if (context.Attendents.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/Attendents.json");

        // Gör om formatet till en lista av typen AttendentModel
        var attendents = JsonSerializer.Deserialize<List<AttendentModel>>(json, options);


        // Kollar så att obejekten är över null/0.
        if (attendents is not null && attendents.Count > 0)
        {

            //             Lägger till hela listan till personer som ska komma. Och sedan sprar det!.
            await context.Attendents.AddRangeAsync(attendents);
            await context.SaveChangesAsync();
        }
    }
}