using Microsoft.EntityFrameworkCore;
using BookFlow.DAL.Context;
using BookFlow.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Controllers
builder.Services.AddControllers();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Dependency Injection

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build
var app = builder.Build();
Console.WriteLine("Starting database migration...");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("Connecting to database...");

        db.Database.Migrate();

        Console.WriteLine("Database migration completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("DATABASE ERROR:");
        Console.WriteLine(ex.ToString());

        throw;
    }
}

Console.WriteLine("Starting application...");


// HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
