using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;
using Movie.Service.Nuget.Interface;
using Movie.Service.Nuget.Repository;
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
                .AddGenericRepository<ApiDbContext, UserMovieReview>()
                .AddMessageBusClient()
                .AddCustomJWTAuthentication();


builder.Services.AddScoped<IMovieReviewRepository, MovieReviewRepository>();
//builder.Services.AddSingleton<Testing>();
//builder.Services.AddScoped(typeof(MessageBusConsumer<>));
//builder.Services.AddSingleton<MessageBusConsumer<EventProcessor>>();
//builder.Services.AddSingleton<TestConsumer<T>>() where T : IEventProcessor; 

builder.Services.AddMessageBusConsumer<ICreatInterface>();
builder.Services.AddMessageBusConsumer<IUpdateInterface>();
builder.Services.AddMessageBusConsumer<IDeleteInterface>();
builder.Services.AddMessageBusConsumer<IAddDirectorInterface>();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<ICreatInterface, EventProcessor>();
builder.Services.AddSingleton<IUpdateInterface, UpdateRepository>();
builder.Services.AddSingleton<IDeleteInterface, DeleteRepository>();
builder.Services.AddSingleton<IAddDirectorInterface, AddDirectorRepository>();




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

