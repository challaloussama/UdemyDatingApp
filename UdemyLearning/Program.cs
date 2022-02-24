using Microsoft.EntityFrameworkCore;
using UdemyLearning.Data;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
string corsName = "corsapp";
builder.Services.AddCors(p => p.AddPolicy(name:corsName, builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsName);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
