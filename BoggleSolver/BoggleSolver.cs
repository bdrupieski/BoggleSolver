using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleSolver
{
    public class BoggleSolver
    {
        private readonly char[][][] _board;
        private readonly Trie _rootTrie;

        private int MinX { get { return 0; } }
        private int MinY { get { return 0; } }
        private int MinZ { get { return 0; } }

        private int MaxX { get { return _board[0][0].Length - 1; } }
        private int MaxY { get { return _board[0].Length - 1; } }
        private int MaxZ { get { return _board.Length - 1; } }

        private readonly List<Point> _directionOffsets = new List<Point>
            {
                new Point(-1, -1, -1), new Point(0, -1, -1), new Point(1, -1, -1),
                new Point(-1, 0, -1),  new Point(0, 0, -1),  new Point(1, 0, -1),
                new Point(-1, 1, -1),  new Point(0, 1, -1),  new Point(1, 1, -1),

                new Point(-1, -1, 0),  new Point(0, -1, 0),  new Point(1, -1, 0),
                new Point(-1, 0, 0),      /* origin */       new Point(1, 0, 0),
                new Point(-1, 1, 0),   new Point(0, 1, 0),   new Point(1, 1, 0),

                new Point(-1, -1, 1),  new Point(0, -1, 1),  new Point(1, -1, 1),
                new Point(-1, 0, 1),   new Point(0, 0, 1),   new Point(1, 0, 1),
                new Point(-1, 1, 1),   new Point(0, 1, 1),   new Point(1, 1, 1),
            };

        public BoggleSolver(IEnumerable<string> words)
        {
            _rootTrie = Trie.BuildTrie(words);
            _board = GenerateBoard();
        }

        public IEnumerable<string> FindWords()
        {
            var foundWords = new List<string>();

            for (int z = 0; z <= MaxZ; z++)
            {
                for (int y = 0; y <= MaxY; y++)
                {
                    for (int x = 0; x <= MaxX; x++)
                    {
                        FindWordsFromPoint(foundWords, new HashSet<Point>(), new Point(x, y, z), "");
                    }
                }
            }

            return foundWords.Distinct();
        }

        private void FindWordsFromPoint(List<string> foundWords, HashSet<Point> visitedPoints, Point currentPoint, string prefix)
        {
            string currentPrefix = prefix + _board[currentPoint.Z][currentPoint.Y][currentPoint.X];

            if (!_rootTrie.ContainsPrefix(currentPrefix))
            {
                return;
            }

            if (_rootTrie.ContainsWord(currentPrefix))
            {
                foundWords.Add(currentPrefix);
            }

            visitedPoints.Add(currentPoint);

            foreach (var offset in _directionOffsets)
            {
                int newX = currentPoint.X + offset.X;
                int newY = currentPoint.Y + offset.Y;
                int newZ = currentPoint.Z + offset.Z;

                if (newX.IsBetweenInclusive(MinX, MaxX) && newY.IsBetweenInclusive(MinY, MaxY) && newZ.IsBetweenInclusive(MinZ, MaxZ))
                {
                    var nextPoint = new Point(newX, newY, newZ);
                    if (!visitedPoints.Contains(nextPoint))
                    {
                        FindWordsFromPoint(foundWords, visitedPoints, nextPoint, currentPrefix);
                    }
                }
            }

            visitedPoints.Remove(currentPoint);
        }

        private char[][][] GenerateBoard()
        {
            return new char[][][]
                {
                    new char[][]
                        {
                            new char[] { 'h', 'e', 'l', 'l', 'o' },
                            new char[] { 'e', 'b', 'c', 'c', 'r' },
                            new char[] { 'f', 'b', 's', 'a', 'f' },
                            new char[] { 'd', 'b', 'h', 'q', 'u' },
                            new char[] { 'y', 'e', 't', 'u', 'q' }
                        },
                    new char[][]
                        {
                            new char[] { 'z', 'h', 'l', 'a', 'o' },
                            new char[] { 'a', 'e', 'k', 'c', 'r' },
                            new char[] { 'f', 'r', 's', 'o', 'i' },
                            new char[] { 'd', 'e', 'h', 'h', 'u' },
                            new char[] { 'o', 't', 'e', 'l', 'g' }
                        },
                    new char[][]
                        {
                            new char[] { 'q', 'u', 'p', 'e', 'f' },
                            new char[] { 'a', 'e', 'c', 'c', 'k' },
                            new char[] { 'z', 's', 'g', 'm', 'j' },
                            new char[] { 's', 'e', 'c', 'p', 'l' },
                            new char[] { 'c', 'i', 'b', 'n', 'g' }
                        },
                    new char[][]
                        {
                            new char[] { 'e', 'u', 'o', 'w', 'e' },
                            new char[] { 'i', 'u', 'y', 't', 'r' },
                            new char[] { 'o', 'p', 'a', 's', 'd' },
                            new char[] { 'k', 'j', 'h', 'e', 'f' },
                            new char[] { 'l', 'm', 'r', 'u', 's' }
                        },
                    new char[][]
                        {
                            new char[] { 'y', 'r', 'w', 'o', 'f' },
                            new char[] { 'e', 's', 'l', 'j', 'h' },
                            new char[] { 'o', 'g', 'h', 'r', 'e' },
                            new char[] { 'i', 'm', 's', 's', 'a' },
                            new char[] { 'q', 'u', 'a', 'v', 'k' }
                        },
                };
        }

        public void DisplayBoard()
        {
            foreach (char[][] plane in _board)
            {
                foreach (char[] row in plane)
                {
                    foreach (char c in row)
                    {
                        Console.Write(c + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

    }
}