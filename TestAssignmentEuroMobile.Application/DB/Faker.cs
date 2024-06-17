using Bogus;
using TestAssignmentEuroMobile.Domain.Models;

namespace TestAssignmentEuroMobile.Application.DB;

public class Faker
{
    public List<Vehicle> vehicles { get; set; }
    public List<Coordinate> coordinates { get; set; }
    private Faker<Vehicle> _vehicleFaker { get; set; }
    private Random _random { get; set; }

    public Faker()
    {
        Randomizer.Seed = new Random(123);
        _random = new Random();

        _vehicleFaker = new Faker<Vehicle>()
            .RuleFor(u => u.VehicleId, _ => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Vehicle.Model());

        long vehicleNumber = _random.NextInt64(1, 100);
        vehicles = _vehicleFaker.Generate((int)vehicleNumber).ToList();


        coordinates = new List<Coordinate>();
        List<Coordinate> fakeCoordinates;
        long id = 1;

        foreach (var vehicle in vehicles)
        {
            long timestamp = _random.NextInt64(0, 10_000);
            Coordinate firstCoordinate = new Coordinate()
            {
                Id = id++,
                Latitude = GetRandomLatitude(),
                Longitude = GetRandomLongitude(),
                VehicleId = vehicle.VehicleId,
                Timestamp = timestamp
            };
            fakeCoordinates = new List<Coordinate> { firstCoordinate };

            for (int i = 0; i < _random.NextInt64(0, 1_000); i++)
            {
                timestamp += 3;
                fakeCoordinates.Add(new Coordinate()
                {
                    Id = id++,
                    Latitude = GetRandomLatitude(),
                    Longitude = GetRandomLongitude(),
                    VehicleId = vehicle.VehicleId,
                    Timestamp = timestamp,
                });
            }
            coordinates.AddRange(fakeCoordinates);
        }
    }

    public double GetRandomLatitude()
    {
        return _random.NextDouble() * 180 - 90;
    }

    public double GetRandomLongitude()
    {
        return _random.NextDouble() * 360 - 180;
    }

}
