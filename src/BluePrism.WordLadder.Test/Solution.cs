using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluePrism.WordLadder.Test
{
    public class Solution
    {

        private IDictionary<string, bool> _dict;
        private IDictionary<string, ICollection<string>> _wildCardsdict;
        private string _root;
        IDictionary<string, ICollection<string>> _listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();
        IDictionary<string, bool> _listOfWords = new Dictionary<string, bool>();

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            foreach (var word in wordList)
            {
                AddValidatedWordAndCreatePreprocessedDictionary(word, word.Length);
            }

            _root = endWord;

            return Solve(beginWord);
        }

        private int Solve(string firstWord)
        {
            var queue = new Queue<string>();
            queue.Enqueue(_root);

            int level = 0;

            while (queue.Count > 0)
            {
                level++;

                string parentWord = queue.Dequeue();

                var similarWords = GetSimiliarWords(parentWord, _wildCardsdict);
                if (!similarWords.Any()) continue;

                foreach (var wordFound in similarWords)
                {
                    if (_dict[wordFound])
                        continue;

                    level++;

                    _dict[wordFound] = true;

                    string newWordFound = wordFound;

                    queue.Enqueue(newWordFound);

                    if (wordFound.Equals(firstWord))
                    {
                        return level;
                    }
                }
            }

            return level;
        }

        private HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords)
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

        private List<string> GetWildcardWords(string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return new List<string>();
            if (self.Length < 2) return new List<string>() { "*" };

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

        private void AddValidatedWordAndCreatePreprocessedDictionary(string line, int permittedLength)
        {
            if (line.Length == permittedLength)
            {
                if (_listOfWords.ContainsKey(line)) return;
                _listOfWords.Add(line, false);

                var preprocessedWords = GetWildcardWords(line);
                foreach (var preprocessedWord in preprocessedWords)
                {
                    if (_listOfPreprocessedWords.ContainsKey(preprocessedWord))
                        _listOfPreprocessedWords[preprocessedWord].Add(line);
                    else
                        _listOfPreprocessedWords.Add(preprocessedWord, new List<string> { line });
                }
            }
        }
    }
}