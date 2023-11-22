using c_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<PrimeInsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=localhost;Database=Prime_insurance_DB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;"));
});
 var _jwtSettings=builder.Configuration.GetSection("jwtSettings");
 builder.Services.Configure<jwtSettings>(_jwtSettings);
var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();




app.Run();
