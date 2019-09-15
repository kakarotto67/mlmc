using Mlmc.Shared.Models;

namespace Mlmc.MGCC.ChipSimulation
{
    internal static class CoordinatesHelper
    {
        /// <summary>
        /// Get distance in km between two GPS points.
        /// </summary>
        internal static double GetDistance(Location start, Location end)
        {
            // TODO: Implement
            return 1000;
        }

        /// <summary>
        /// Get GPS point on a way from start to end with set distance from start.
        /// </summary>
        internal static Location GetIntermediateLocation(Location start, Location end, double distanceFromStart)
        {
            // TODO: Implement
            return new Location();
        }
    }
}