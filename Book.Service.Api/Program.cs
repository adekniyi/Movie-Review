using Book.Service.Api.Data;
using Book.Service.Api.Interface;
using Book.Service.Api.Model;
using Book.Service.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;
//using Movie.Service.Nuget.Interface;
//using Movie.Service.Nuget.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase<ApiDbContext>(builder.Configuration.GetConnectionString("Database"))
                .AddGenericRepository<ApiDbContext, Book.Service.Api.Model.Movie>()
                .AddGenericRepository<ApiDbContext, Director>()
                .AddMessageBusClient();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();

//builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
//});
var app = builder.Build();

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

