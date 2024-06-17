using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAssignmentEuroMobile.Application.Abstract;
using TestAssignmentEuroMobile.Application.DB;
using TestAssignmentEuroMobile.Application.Services;
using TestAssignmentEuroMobile.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


#region Enpoints
app.MapGet("/vehicles", async ([FromServices] IVehicleService service) =>
{
    return await service.GetAllVehiclesAsync();
})
.WithName("Vehicles")
.WithOpenApi();



app.MapPost("/coordinates/find", async ([FromBody] List<Guid> guids, [FromServices] IVehicleService service) =>
{
    return await service.GetAllCoordinatesByIdsAsync(guids);
})
.WithName("Find")
.WithOpenApi();


app.MapPost("/coordinates/calculate_path", async ([FromBody] List<Coordinate> coordinates, [FromServices] IVehicleService service) =>
{
    return await service.CalculatePathForVehiclesAsync(coordinates);
})
.WithName("Calculate_path")
.WithOpenApi();
#endregion Enpoints


app.Run();