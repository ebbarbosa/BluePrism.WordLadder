using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluePrism.WordLadder.Test
{
    public class Solution
    {

        private string _root;
        private readonly IDictionary<string, ICollection<string>> _listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();
        private readonly IDictionary<string, bool> _listOfWords = new Dictionary<string, bool>();

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if (!wordList.Contains(endWord)) return 0;

            AddValidatedWordAndCreatePreprocessedDictionary(beginWord);
            _listOfWords[beginWord] = true;
            foreach (var word in wordList)
            {
                AddValidatedWordAndCreatePreprocessedDictionary(word);
            }

            _root = beginWord;

            return Solve(endWord);
        }

        private int Solve(string targetWord)
        {
            var queue = new Queue<Tuple<string, int>>();
            queue.Enqueue(new Tuple<string, int>(_root, 1));

            while (queue.Count > 0)
            {
                var parentWord = queue.Dequeue();

                var adjacentWords = GetSimiliarWords(parentWord.Item1, _listOfPreprocessedWords);
                if (!adjacentWords.Any()) continue;

                foreach (var adjacentWord in adjacentWords)
                {
                    if (_listOfWords[adjacentWord])
                        continue;

                    Tuple<string, int> newWordFound = new Tuple<string, int>(adjacentWord, parentWord.Item2 + 1);

                    if (newWordFound.Item1.Equals(targetWord, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return newWordFound.Item2;
                    }

                    _listOfWords[adjacentWord] = true;
                    
                    queue.Enqueue(newWordFound);
                }
            }

            return 0;
        }

        private static HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords)
        {
            var wildcardWords = GetWildcardWords(word);
            var words = new HashSet<string>();

            foreach (var wildcardWord in wildcardWords)
            {
                var nodesFound = preprocessedWords[wildcardWord];
                words.UnionWith(nodesFound);
            }

            return words;
        }

        private static List<string> GetWildcardWords(string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return new List<string>();

            var result = new List<string>();

            var newWord = self;
            for (int letterIndex = 0; letterIndex < self.Length; letterIndex++)
            {
                var wildCardString = new StringBuilder(newWord) { [letterIndex] = '*' };
                newWord = wildCardString.ToString();

                result.Add(newWord);
                newWord = self;
            }

            return result;
        }

        private void AddValidatedWordAndCreatePreprocessedDictionary(string line)
        {

            if (!_listOfWords.ContainsKey(line))
                _listOfWords.Add(line, false);

            var preprocessedWords = GetWildcardWords(line);
            foreach (var preprocessedWord in preprocessedWords)
            {
                if (_listOfPreprocessedWords.ContainsKey(preprocessedWord))
                {
                    if (!_listOfPreprocessedWords[preprocessedWord].Contains(line))
                    {
                        _listOfPreprocessedWords[preprocessedWord].Add(line);
                    }
                }
                else
                    _listOfPreprocessedWords.Add(preprocessedWord, new List<string> { line });
            }
        }
    }
}