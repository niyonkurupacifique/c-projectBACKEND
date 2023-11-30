
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using c_.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<PrimeInsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=10.10.1.44;Database=IngenziDB;User Id=sa;Password=inrichTECH789;TrustServerCertificate=True;Connection Timeout=18000000;"));
});

var _jwtSettings = builder.Configuration.GetSection("jwtSettings");
builder.Services.Configure<jwtSettings>(_jwtSettings);

var app = builder.Build();

// Enable CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<PreferredLanguageMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();

app.Run();
