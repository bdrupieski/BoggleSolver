using System;

namespace BoggleSolver
{
    public static class ComparableExtensions
    {
        public static bool IsBetweenInclusive<T>(this T val, T min, T max) where T : IComparable<T>
        {
            return val.CompareTo(min) >= 0 && val.CompareTo(max) <= 0;
        }
    }
}