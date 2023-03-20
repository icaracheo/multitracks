using api.multitracks.com.Interfaces;
using api.multitracks.com.Models;
using api.multitracks.com.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

// Add services to the container.
builder.Services.AddScoped<IMultitracksProvider, SqlServerMultitracksProvider>();
builder.Services.AddControllers();
builder.Services.AddDbContext<MultiTracksDBContext>(options =>
{
    options.UseSqlServer(config.GetSection("ConnectionString").Value);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
