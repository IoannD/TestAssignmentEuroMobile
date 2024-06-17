using Microsoft.EntityFrameworkCore;
using TestAssignmentEuroMobile.Application.Abstract;
using TestAssignmentEuroMobile.Application.DB;
using TestAssignmentEuroMobile.Application.Dtos;
using TestAssignmentEuroMobile.Domain.Models;
using TestAssignmentEuroMobile.Application.Common;

namespace TestAssignmentEuroMobile.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly Context _context;
    public VehicleService(Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<IEnumerable<CoordinateDto>> GetAllCoordinatesByIdsAsync(IList<Guid> guids)
    {
        List<Coordinate> coordinates = await _context.Coordinates
            .Where(x => guids.Contains(x.VehicleId))
            .OrderBy(x => x.Vehicle.Name)
            .ThenByDescending(x => x.Timestamp)
            .ToListAsync();

        List<CoordinateDto> coordinatesDtos = new List<CoordinateDto>(coordinates.Count());
        foreach (var coordinate in coordinates)
        {
            coordinatesDtos.Add(new CoordinateDto()
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude,
                Timestamp = coordinate.Timestamp,
                VehicleId = coordinate.VehicleId,
            });
        }

        return coordinatesDtos;
    }

    public async Task<Dictionary<string, PathDto>> CalculatePathForVehiclesAsync(List<Coordinate> coordinates)
    {
        Dictionary <Guid, double> paths = new Dictionary<Guid, double>();
        List<Guid> guids = new List<Guid>();
        var vehicleCoordinates = coordinates.GroupBy(x => x.VehicleId);
        foreach (var vehicleCoordinate in vehicleCoordinates)
        {
            guids.Add(vehicleCoordinate.Key);
            paths[vehicleCoordinate.Key] = CalculatePath(vehicleCoordinate.ToList());
        }


        IEnumerable<Vehicle> vehicles = await _context.Vehicles
            .Where(x => guids.Contains(x.VehicleId))
            .ToListAsync();

        Dictionary<string, PathDto> result = new Dictionary<string, PathDto>();
        foreach (var vehicle in vehicles)
        {
            result[vehicle.Name] = new PathDto()
            {
                Metres = paths[vehicle.VehicleId],
                Miles = paths[vehicle.VehicleId] * 0.000621371
            };
        }
        return result;
    }


    public double CalculatePath(List<Coordinate> coordinates)
    {
        double path = 0.0;
        if (coordinates.Count() == 0) return path;

        Coordinate currCoord = coordinates[0];
        for (int i = 1; i < coordinates.Count(); i++)
        {
            path += Utils.CalculateDistance(currCoord, coordinates[i]);
            currCoord = coordinates[i];
        }

        return path;
    }
}

