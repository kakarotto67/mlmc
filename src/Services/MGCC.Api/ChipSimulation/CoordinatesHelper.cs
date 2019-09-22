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
        internal static double GetDistance(Location from, Location to)
        {
            var differenceLatitudeRadian = (to.Latitude - from.Latitude) * RadianCoeff;
            var differenceLongitudeRadian = (to.Longitude - from.Longitude) * RadianCoeff;

            var startLatitudeRadian = from.Latitude * RadianCoeff;
            var endLatitudeRadian = to.Latitude * RadianCoeff;

            var a = Math.Sin(differenceLatitudeRadian / 2) * Math.Sin(differenceLatitudeRadian / 2) +
                    Math.Sin(differenceLongitudeRadian / 2) * Math.Sin(differenceLongitudeRadian / 2) *
                    Math.Cos(startLatitudeRadian) * Math.Cos(endLatitudeRadian);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = c * EarthRadiusKm;
            return distance;
        }

        /// <summary>
        /// Get GPS point on a way from A to B based on A coordinates, bearing and distance.
        /// </summary>
        internal static Location GetIntermediateLocation(Location from, double bearing, double distance)
        {
            // https://www.movable-type.co.uk/scripts/latlong.html

            if (from == null)
            {
                return null;
            }

            // Find intermediate location based on to, bearing and distance
            return FindDestinationForGivenStartPointAndBearing(from, bearing, distance);
        }

        public static double FindInitialBearing(Location from, Location to)
        {
            if (from == null || to == null)
            {
                return 0;
            }

            var fromLatRadian = from.Latitude * RadianCoeff;
            var toLatRadian = to.Latitude * RadianCoeff;
            var longitudeDeltaRadian = (to.Longitude - from.Longitude) * RadianCoeff;

            var x = Math.Cos(fromLatRadian) * Math.Sin(toLatRadian)
                - Math.Sin(fromLatRadian) * Math.Cos(toLatRadian) * Math.Cos(longitudeDeltaRadian);
            var y = Math.Sin(longitudeDeltaRadian) * Math.Cos(toLatRadian);
            var t = Math.Atan2(y, x);

            var bearing = t * DegreesCoeff;

            return Wrap360(bearing);
        }

        private static Location FindDestinationForGivenStartPointAndBearing(Location from,
            double bearing, double distance)
        {
            if (from == null)
            {
                return null;
            }

            // Angular distance in radians
            var angular = distance / EarthRadiusKm;
            var bearingRadian = bearing * RadianCoeff;

            var latitudeRadian = from.Latitude * RadianCoeff;
            var longitudeRadian = from.Longitude * RadianCoeff;

            var destLatSine = Math.Sin(latitudeRadian) * Math.Cos(angular)
                + Math.Cos(latitudeRadian) * Math.Sin(angular) * Math.Cos(bearingRadian);
            var destLatitudeRadian = Math.Asin(destLatSine);

            var y = Math.Sin(bearingRadian) * Math.Sin(angular) * Math.Cos(latitudeRadian);
            var x = Math.Cos(angular) - Math.Sin(latitudeRadian) * destLatSine;
            var destLongitudeRadian = longitudeRadian + Math.Atan2(y, x);

            var destLatitude = destLatitudeRadian * DegreesCoeff;
            var destLongitude = destLongitudeRadian * DegreesCoeff;

            return new Location
            {
                Latitude = Math.Round(Wrap90(destLatitude), 6),
                Longitude = Math.Round(Wrap180(destLongitude), 6)
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