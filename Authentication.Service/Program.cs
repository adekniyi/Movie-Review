using Authentication.Service.Data;
using Authentication.Service.Interface;
using Authentication.Service.Model;
using Authentication.Service.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Scoped)
    .AddCustomJWTAuthentication()
    .AddServiceLifeScope(); ;


builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApiContext>();
                //.AddDefaultTokenProviders();

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer();

//builder.Services.AddScoped<IJWTRepository, JWTRepository>();


builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

