using EventApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Setup database connection to Sqlite

builder.Services.AddDbContext<EventApiContext>(options => {
options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Seed the database ...
/*
DEPENDENCY INJECTION - Egen skapad

using - Gör att det städas bort automatiskt när det inte används längre.
*/
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<EventApiContext>();

    await context.Database.MigrateAsync();

    await SeedData.LoadEvents(context);
    await SeedData.LoadAttendents(context);


}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);

    // Stanna appen om den krashar!
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
