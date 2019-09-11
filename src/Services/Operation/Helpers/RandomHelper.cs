using System;

namespace Operation.Helpers
{
    internal static class RandomHelper
    {
        private const int Min = 100;
        private const int Max = Int32.MaxValue;

        internal static Int32 GetTrueRandom()
        {
            return (new Random((Int32)DateTime.UtcNow.Ticks)).Next(Min, Max);
        }
    }
}