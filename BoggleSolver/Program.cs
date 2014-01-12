using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BoggleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lotsOfWords = File.ReadAllLines("2of4brif.txt");

            var solver = new BoggleSolver(lotsOfWords);

            Console.WriteLine("Board:");
            solver.DisplayBoard();

            var sw = Stopwatch.StartNew();
            var foundWords = solver.FindWords().ToList();
            sw.Stop();

            //Console.WriteLine("Words found:");
            //Console.WriteLine(string.Join(Environment.NewLine, foundWords));

            Console.WriteLine("found {0} words", foundWords.Count);
            Console.WriteLine("took {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
