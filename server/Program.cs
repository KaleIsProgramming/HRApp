using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HolidayRequestApi.Data;

var builder = WebApplication.CreateBuilder(args);

//Controllery
builder.Services.AddControllers();

//EntityFramework do zapisu
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("HolidayRequestsDb"));

//Ustawienia Cors'ów
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
//Swager
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HolidayRequestApi", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();