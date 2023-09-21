using agilt_projekt.web.Data;
using agilt_projekt.web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add dataBase support....
builder.Services.AddDbContext<EventsContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// seed the DB
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try{
        var context = services.GetRequiredService<EventsContext>();
        await context.Database.MigrateAsync();
        await SeedData.LoadEventData(context);
    }
    catch (Exception ex){
       Console.WriteLine("{0} - {1}", ex.Message, ex.InnerException!.Message);
       throw;
    }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
