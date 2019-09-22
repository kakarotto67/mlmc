using Mlmc.Shared.Models;
using System;

namespace Mlmc.MGCC.Api.ChipSimulation
{
    internal static class CoordinatesHelper
    {
        private const int EarthRadiusKm = 6371;
        private const double RadianCoeff = Math.PI / 180;
        private const double DegreesCoeff = 180 / Math.PI;

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
            // https://www.movable-type.co.uk/scripts/latlong.html

            // φ is latitude, λ is longitude

            // I. Bearing to Find Bearing
            var bearing = FindInitialBearing(lat1, lon1, lat2, lon2);

            // II. Then 'Destination point given distance and bearing from start point'
            // to find the point

            return FindDestinationForGivenStartPointAndBearing(lat1, lon1, bearing, dist);
        }

        private static double FindInitialBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var f1 = lat1 * RadianCoeff;
            var f2 = lat2 * RadianCoeff;
            var lonDelta = (lon2 - lon1) * RadianCoeff;

            var x = Math.Cos(f1) * Math.Sin(f2) - Math.Sin(f1) * Math.Cos(f2) * Math.Cos(lonDelta);
            var y = Math.Sin(lonDelta) * Math.Cos(f2);
            var t = Math.Atan2(y, x);

            var bearing = t * DegreesCoeff;

            return Wrap360(bearing);
        }

        private static Location FindDestinationForGivenStartPointAndBearing(double lat1, double lon1,
            double bearing, double distance)
        {
            var radius = 6371;// e3;

            var angular = distance / radius; // angular distance in radians
            var t = bearing * RadianCoeff;

            var f1 = lat1 * RadianCoeff;
            var l1 = lon1 * RadianCoeff;

            var sinf2 = Math.Sin(f1) * Math.Cos(angular) + Math.Cos(f1) * Math.Sin(angular) * Math.Cos(t);
            var f2 = Math.Asin(sinf2);
            var y = Math.Sin(t) * Math.Sin(angular) * Math.Cos(f1);
            var x = Math.Cos(angular) - Math.Sin(f1) * sinf2;
            var l2 = l1 + Math.Atan2(y, x);

            var lat = f2 * DegreesCoeff;
            var lon = l2 * DegreesCoeff;

            return new Location
            {
                Latitude = Math.Round(Wrap90(lat), 6),
                Longitude = Math.Round(Wrap180(lon), 6)
            };
        }

        private static double Wrap360(double degrees)
        {
            // Avoid rounding due to arithmetic ops if within range
            if (0 <= degrees && degrees < 360)
            {
                return degrees;
            }

            // Sawtooth wave p:360, a:360
            return (degrees % 360 + 360) % 360;
        }

        private static double Wrap180(double degrees)
        {
            // Avoid rounding due to arithmetic ops if within range
            if (-180 < degrees && degrees <= 180)
            {
                return degrees;
            }

            // Sawtooth wave p:180, a:±180
            return (degrees + 540) % 360 - 180;
        }

        private static double Wrap90(double degrees)
        {
            // Avoid rounding due to arithmetic ops if within range
            if (-90 <= degrees && degrees <= 90)
            {
                return degrees;
            }

            // Triangle wave p:360 a:±90 TODO: fix e.g. -315°
            return Math.Abs((degrees % 360 + 270) % 360 - 180) - 90;
        }
    }
}