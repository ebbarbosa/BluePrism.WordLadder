using BluePrism.WordLadder.Models.Extensions;
using System;
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

        public IList<string> SolveLadder(string firstWord, string targetWord, IDictionary<string, bool> wordDictionary)
        {
            _root = new Word(targetWord);
            _dict = wordDictionary;

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
                GenerateChildren(newWord, firstWord, queue);
                if (_target != null)
                {
                    break;
                }
            }
        }

        private void GenerateChildren(Word parent, string targetWord, Queue<Word> queue)
        {
            string parentWord = parent.WordKey;
            List<string> lstWords = _dict.Keys.ToList();
            foreach (string word in lstWords)
            {
                if (_dict[word])
                    continue;

                if (word.IsDifferentOnlyBy(parentWord, Constants.NumCharsToDiffer))
                {
                    _dict[word] = true;
                    Word newWordFound = new Word(word, parent);

                    queue.Enqueue(newWordFound);
                    if (word.Equals(targetWord))
                    {
                        _target = newWordFound;
                        return;
                    }
                }
            }
        }
    }
}