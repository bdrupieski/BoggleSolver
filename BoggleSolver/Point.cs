using System;
using System.Diagnostics;

namespace BoggleSolver
{
    public class Point : IEquatable<Point>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        [DebuggerStepThrough]
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y: {1} Z: {2}", X, Y, Z);
        }

        public bool Equals(Point other)
        {
            return Z == other.Z && Y == other.Y && X == other.X;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Z;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ X;
                return hashCode;
            }
        }
    }
}