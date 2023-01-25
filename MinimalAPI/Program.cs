using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1"));
}

app.MapGet("/", () => "Hello world!");

app.MapGet("sentences", async () =>
    await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes")
);

app.Run();