using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;
using MovieReview.Api.Data;
using MovieReview.Api.Interface;
using MovieReview.Api.Model;
using MovieReview.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddDatabase<ApiDbContext>(builder.Configuration.GetConnectionString("Database"))
                .AddGenericRepository<ApiDbContext, MovieReview.Api.Model.Movie>()
                .AddGenericRepository<ApiDbContext, UserMovieReview>();


builder.Services.AddScoped<IMovieReviewRepository, MovieReviewRepository>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddSingleton<Testing>();
builder.Services.AddSingleton(typeof(TestConsumer<>));
//builder.Services.AddSingleton<TestConsumer<T>>() where T : IEventProcessor; 

builder.Services.AddHostedService<MessageBusSubscriber>();

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

