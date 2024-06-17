using GeoCoordinatePortable;
using TestAssignmentEuroMobile.Domain.Models;

namespace TestAssignmentEuroMobile.Application.Common
{
    public static class Utils
    {
        public static double CalculateDistance(Coordinate firstCoordinate, Coordinate secondCoordinate)
        {
            GeoCoordinate geo_1 = new GeoCoordinate(firstCoordinate.Latitude, firstCoordinate.Longitude);
            GeoCoordinate geo_2 = new GeoCoordinate(secondCoordinate.Latitude, secondCoordinate.Longitude);
            return geo_1.GetDistanceTo(geo_2);
        }
    }
}
