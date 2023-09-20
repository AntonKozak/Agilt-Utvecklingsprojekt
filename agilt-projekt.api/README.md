# Sätta upp databas



### EntityFrameworkCore
Ladda ner `Microsoft.EntityFrameworkCore`.

`Microsoft.EntityFrameworkCore.Tools` ( För att göra migreringar )


## SqlLite enkel databas för utveckling

`Microsoft.EntityFrameworkCore.Sqlite`



När modellerna är byggda och databasen är konfigurerad i Program.cs

```
builder.Services.AddDbContext<EvenApiContext>(options => {
options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});
```

Kör dessa kommandon i terminalen:

`dotnet ef migrations add InitialtCreate -o Data/Migrations`

`dotnet ef database update`


## Nu kommer Foreign-key, Relationships mapper.

## AUTH

`Microsoft.AspNetCore.Authentication.JwtBearer`

`Microsoft.AspNetCore.Identity.EntityFrameWorkCore`

```
// Lägger till identity med roller!
builder.Services.AddIdentityCore<UserModel>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<EventApiContext>();
```