using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TestAssignmentEuroMobile.Domain.Models;

namespace TestAssignmentEuroMobile.Application.DB;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        try
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .HasKey(x => x.VehicleId);

        modelBuilder.Entity<Coordinate>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Vehicle>()
            .Property(x => x.Name).HasMaxLength(30);

        
        var faker = new Faker();
        modelBuilder.Entity<Vehicle>()
            .HasData(faker.vehicles);

        modelBuilder.Entity<Coordinate>()
            .HasData(faker.coordinates);
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }

}