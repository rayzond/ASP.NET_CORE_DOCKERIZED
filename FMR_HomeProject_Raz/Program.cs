using AirfarePriceAlertSystem.Data;
using AirfarePriceAlertSystem.Models;
using AirfarePriceAlertSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<UserAlertDAO>();
builder.Services.AddScoped<UserAlertService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed Mock Data (if needed)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedMockData(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


void SeedMockData(ApplicationDbContext context)
{
    if (!context.Users.Any())
    {
        var users = new List<User>
        {
            new User { FirstName = "Alice", LastName = "Johnson", Age = 30, Country = "USA", Currency = "USD" },
            new User { FirstName = "Bob", LastName = "Smith", Age = 25, Country = "UK", Currency = "GBP" },
            new User { FirstName = "Charlie", LastName = "Brown", Age = 35, Country = "Canada", Currency = "CAD" }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var userAlerts = new List<UserAlert>
        {
            new UserAlert { UserUID = 1, From = "JFK", To = "LAX", MaxPrice = 300 },
            new UserAlert { UserUID = 1, From = "JFK", To = "IL-TLV", MaxPrice = 350 },
            new UserAlert { UserUID = 2, From = "LHR", To = "JFK", MaxPrice = 500 },
            new UserAlert { UserUID = 3, From = "YYZ", To = "SFO", MaxPrice = 400 }
        };

        context.UserAlerts.AddRange(userAlerts);
        context.SaveChanges();
    }
}

