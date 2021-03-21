using BluePrism.WordLadder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BluePrism.WordLadder.Models
{
    public class GraphLadder
    {

        private readonly Word _root;
        private Word _target = null;
        private readonly IDictionary<string, bool> _dict;

        public GraphLadder(string source, IDictionary<string, bool> dict)
        {
            _root = new Word(source);
            _dict = dict;
        }

        private void GenerateChildren(Word parent, string targetWord, Queue<Word> queue)
        {
            string parentWord = parent.WordKey;
            List<string> lstWords = _dict.Keys.ToList();
            foreach (string word in lstWords)
            {
                if (_dict[word])
                    continue;

                if (word.IsSimilarBy(parentWord, Constants.NumCharsToDiffer))
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

        public IList<string> Solve(string targetWord)
        {
            var queue = new Queue<Word>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                Word newWord = queue.Dequeue();
                GenerateChildren(newWord, targetWord, queue);
                if (_target != null)
                {
                    break;
                }
            }

            if (_target == null)
            {
                Console.WriteLine("Ladder not found");
                return Enumerable.Empty<string>().ToList();
            }

            return _target.ToList();
        }
    }
}