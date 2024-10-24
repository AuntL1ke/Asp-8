using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICarService,CarService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddDbContext<CarDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("CarDbContext"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
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
