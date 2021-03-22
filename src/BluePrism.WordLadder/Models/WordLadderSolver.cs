using BluePrism.WordLadder.Models.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BluePrism.WordLadder.Models
{
    public class WordLadderSolver : IWordLadderSolver
    {
        /// <summary>
        /// root will hold the last word of the ladder and its parent.
        /// </summary>
        private Word _root;

        /// <summary>
        /// target holds the value of the last word to be found. Once target is found we have completed the ladder.
        /// </summary>
        private Word _target = null;

        /// <summary>
        /// This dictionary works as a memoizing object, to remeber the parent from a word already examined.
        /// </summary>
        private IDictionary<string, bool> _dict;

        private IList<string> _result;

        public IList<string> SolveLadder(string firstWord, string targetWord, HashSet<string> listOfWord)
        {
            _root = new Word(targetWord);
            _dict = listOfWord.ToDictionary(key => key, _ => false);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Solve(firstWord);
            sw.Stop();
            Console.WriteLine("Time taken with graphs = {0} ms", sw.ElapsedMilliseconds);

            if (_target == null)
            {
                Console.WriteLine("Ladder not found");
                return Enumerable.Empty<string>().ToList();
            }

            _result = _target.ToList();

            return _result;
        }

        public void Solve(string firstWord)
        {
            var queue = new Queue<Word>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                Word newWord = queue.Dequeue();

                string parentWord = newWord.WordKey;
                var lstWords = _dict;
                foreach (string word in lstWords.Keys)
                {
                    if (_dict[word])
                        continue;

                    if (word.IsDifferentOnlyBy(parentWord, Constants.NumCharsToDiffer))
                    {
                        _dict[word] = true;

                        Word newWordFound = new Word(word, newWord);
                        queue.Enqueue(newWordFound);

                        if (word.Equals(firstWord))
                        {
                            _target = newWordFound;
                            return;
                        }
                    }
                }

                if (_target != null)
                {
                    break;
                }
            }
        }
    }
}