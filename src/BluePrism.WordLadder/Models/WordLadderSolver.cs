using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using BluePrism.WordLadder.Extensions;

namespace BluePrism.WordLadder.Models
{
    public class WordLadderSolver : IWordLadderSolver
    {
        public IList<string> SolveLadder(string firstWord, string targetWord, IDictionary<string, bool> wordDictionary)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            GraphLadder graph = new GraphLadder(firstWord, wordDictionary);
            var result = graph.Solve(targetWord);
            sw.Stop();
            Console.WriteLine("Time taken with graphs = {0} ms", sw.ElapsedMilliseconds);

            return result.Reverse().ToList();
        }
    }
}