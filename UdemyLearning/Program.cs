using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UdemyLearning.Data;
using UdemyLearning.Interfaces;
using UdemyLearning.Services;
using UdemyLearning.Extensions;
using UdemyLearning.Middleware;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.addApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string corsName = "corsapp";
builder.Services.AddCors(p => p.AddPolicy(name:corsName, builder =>
{  
    builder.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(corsName);

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
