using Mlmc.Shared.Models;
using System;

namespace Mlmc.MGCC.Api.ChipSimulation
{
    internal static class CoordinatesHelper
    {
        private const int EarthRadiusKm = 6371;
        private const double RadianCoeff = Math.PI / 180;

        /// <summary>
        /// Get distance in km between two GPS points.
        /// </summary>
        internal static double GetDistance(Location start, Location end)
        {
            // TODO: Test this implementation
            var differenceLatitudeRadian = (end.Latitude - start.Latitude) * RadianCoeff;
            var differenceLongitudeRadian = (end.Longitude - start.Longitude) * RadianCoeff;

            var startLatitudeRadian = start.Latitude * RadianCoeff;
            var endLatitudeRadian = end.Latitude * RadianCoeff;

            var a = Math.Sin(differenceLatitudeRadian / 2) * Math.Sin(differenceLatitudeRadian / 2) +
                    Math.Sin(differenceLongitudeRadian / 2) * Math.Sin(differenceLongitudeRadian / 2) *
                    Math.Cos(startLatitudeRadian) * Math.Cos(endLatitudeRadian);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = c * EarthRadiusKm;
            return distance;
        }

        /// <summary>
        /// Get GPS point on a way from start to end with set distance from start.
        /// </summary>
        internal static Location GetIntermediateLocation(Location start, Location end, double distanceFromStart)
        {
            // TODO: Test this implementation
            var angular = distanceFromStart / EarthRadiusKm;

            var a = Math.Sin(0 * angular) / Math.Sin(angular);
            var b = Math.Sin(1 * angular) / Math.Sin(angular);

            var startLatitudeRadian = start.Latitude * RadianCoeff;
            var startLongitudeRadian = start.Longitude * RadianCoeff;
            var endLatitudeRadian = end.Latitude * RadianCoeff;
            var endLongitudeRadian = end.Longitude * RadianCoeff;

            var x = a * Math.Cos(startLatitudeRadian) * Math.Cos(startLongitudeRadian) +
                       b * Math.Cos(endLatitudeRadian) * Math.Cos(endLongitudeRadian);
            var y = a * Math.Cos(startLatitudeRadian) * Math.Sin(startLongitudeRadian) +
                       b * Math.Cos(endLatitudeRadian) * Math.Sin(endLongitudeRadian);
            var z = a * Math.Sin(startLatitudeRadian) + b * Math.Sin(endLatitudeRadian);

            var intermediateLatitude = Math.Atan2(z, Math.Sqrt(x * x + y * y));
            var intermediateLongitude = Math.Atan2(y, x);

            return new Location
            {
                Latitude = Math.Round(intermediateLatitude / RadianCoeff, 6),
                Longitude = Math.Round(intermediateLongitude / RadianCoeff, 6)
            };
        }

        internal static Location GetIntermediateLocation(double lat1, double lon1,
                                       double lat2, double lon2, double dist)
        {
            double constant = Math.PI / 180;
            double angular = dist / 6371;

            double a = Math.Sin(0 * angular) / Math.Sin(angular);
            double b = Math.Sin(1 * angular) / Math.Sin(angular);

            double x = a * Math.Cos(lat1 * constant) * Math.Cos(lon1 * constant) +
                       b * Math.Cos(lat2 * constant) * Math.Cos(lon2 * constant);
            double y = a * Math.Cos(lat1 * constant) * Math.Sin(lon1 * constant) +
                       b * Math.Cos(lat2 * constant) * Math.Sin(lon2 * constant);
            double z = a * Math.Sin(lat1 * constant) + b * Math.Sin(lat2 * constant);

            double lat3 = Math.Atan2(z, Math.Sqrt(x * x + y * y));
            double lon3 = Math.Atan2(y, x);

            return new Location
            {
                Latitude = Math.Round(lat3 / constant, 6),
                Longitude = Math.Round(lon3 / constant, 6)
            };
        }
    }
}