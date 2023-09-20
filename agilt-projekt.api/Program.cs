using System.Text;
using EventApi.Auth;
using EventApi.Data;
using EventApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Setup database connection to Sqlite

builder.Services.AddDbContext<EventApiContext>(options => {
options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

// Lägger till identity med roller!
builder.Services.AddIdentityCore<UserModel>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<EventApiContext>();



// Lägga till TokenService
// AddScoped gör den giltig under hela sessionen.
builder.Services.AddScoped<TokenService>();


// Lägger till tokens - JWT Payloads
builder.Services.AddScoped<TokenService>();

// Sätter upp schema för tokens - JWT!
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenSettings:tokenKey").Value))
        /*ValidIssuer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenSetting:issuer").Value)).ToString() */
    };
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

    // Identity
    var userMgr = services.GetRequiredService<UserManager<UserModel>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

    await context.Database.MigrateAsync();

    await SeedData.LoadRolesAndUsers(userMgr,roleMgr);

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


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
