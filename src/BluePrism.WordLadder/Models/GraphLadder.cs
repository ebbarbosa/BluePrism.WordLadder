using BluePrism.WordLadder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BluePrism.WordLadder.Models
{
    public class GraphLadder
    {

        public Word root;
        public Word target = null;
        private readonly IDictionary<string, bool> _dict;

        public GraphLadder(string source, IDictionary<string, bool> dict)
        {
            root = new Word(source);
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
                        target = newWordFound;
                        return;
                    }
                }
            }
        }

        public IList<string> Solve(string targetWord)
        {
            var queue = new Queue<Word>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Word newWord = queue.Dequeue();
                GenerateChildren(newWord, targetWord, queue);
                if (target != null)
                {
                    break;
                }
            }

            if (target == null)
            {
                Console.WriteLine("Ladder not found");
                return Enumerable.Empty<string>().ToList();
            }

            return target.ToList();
        }

    }
}