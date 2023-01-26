using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalAPI.Domain;
using MinimalAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ToDosDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1"));
}


// Endpoints

app.MapGet("/todos", async (AppDbContext db) => await db.ToDos.ToListAsync());

app.MapGet("/todos/{id}", async (int id, AppDbContext db) =>
    await db.ToDos.FindAsync(id) is { } toDo ? Results.Ok(toDo) : Results.NotFound()
);

app.MapGet("/todos/completed", async (AppDbContext db) => await db.ToDos.Where(t => t.IsCompleted).ToListAsync());

app.MapPost("/todos", async (ToDo todo, AppDbContext db) =>
{
    db.ToDos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.Run();