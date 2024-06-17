using TestAssignmentEuroMobile.Application.Dtos;
using TestAssignmentEuroMobile.Domain.Models;

namespace TestAssignmentEuroMobile.Application.Abstract
{
    public interface IVehicleService
    {
        public Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        public Task<IEnumerable<CoordinateDto>> GetAllCoordinatesByIdsAsync(IList<Guid> guids);
        public Task<Dictionary<string, PathDto>> CalculatePathForVehiclesAsync(List<Coordinate> coordinates);
    }
}
