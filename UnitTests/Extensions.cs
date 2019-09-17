using System;

namespace UnitTests
{
    static class Extensions
    {
        /// <summary>
        /// Compares doubles with a sensible tolerance
        /// </summary>
        /// <param name="left">The first value in the comparison</param>
        /// <param name="right">The second value in the comparison</param>
        /// <param name="tolerance">The tolerance to use in the comparison (0.0000001 by default)</param>
        /// <returns>Whether the values are within </returns>
        public static bool IsCloseEnoughTo(this double left, double right, double tolerance = 0.0000001)
        {
            return Math.Abs(left - right) < tolerance;
        }
    }
}
