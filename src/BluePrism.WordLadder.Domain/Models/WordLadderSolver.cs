using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BluePrism.WordLadder.Domain.Models.Extensions;

namespace BluePrism.WordLadder.Domain.Models
{
    public class WordLadderSolver : IWordLadderSolver
    {
        private readonly IGetSimilarWordsFromProcessedListService _getWordFromProcessedListService;

        /// <summary>
        /// root will hold the last word of the ladder and its parent.
        /// </summary>
        private Word _root;

        /// <summary>
        /// target holds the value of the last word to be found. Once target is found we have completed the ladder.
        /// </summary>
        private Word _target = null;

        /// <summary>
        /// This dictionary works as a memoizing object, to remeber the words already examined.
        /// </summary>
        private IDictionary<string, bool> _dict;

        /// <summary>
        /// This dictionary works as a preprocessed word dictionary, it has all possible transformations with wildcards and the index of the word in the dictionary above.
        /// So when the lookup founds a match It knows the index of the word in the dictionary above.
        /// </summary>
        private IDictionary<string, ICollection<string>> _wildCardsdict;

        private IList<string> _result;

        public WordLadderSolver(IGetSimilarWordsFromProcessedListService getWordFromProcessedListService)
        {
            _getWordFromProcessedListService = getWordFromProcessedListService;
        }

        public IList<string> SolveLadder(string firstWord, string targetWord,
            IDictionary<string, bool> wordDictionaryVisited,
            IDictionary<string, ICollection<string>> wordOfPreprocessedWords)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            _root = new Word(targetWord);
            _dict = wordDictionaryVisited;
            _wildCardsdict = wordOfPreprocessedWords;
            Solve(firstWord);
            
            if (_target == null)
            {
                Console.WriteLine("Ladder not found");
                return Enumerable.Empty<string>().ToList();
            }

            _result = _target.ToList();

            sw.Stop();
            Console.WriteLine("Time taken with graphs = {0} ms", sw.ElapsedMilliseconds);

            return _result;
        }

        private void Solve(string firstWord)
        {
            var queue = new Queue<Word>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                Word newWord = queue.Dequeue();

                string parentWord = newWord.WordKey;

                var similarWords = _getWordFromProcessedListService.GetSimiliarWords(parentWord, _wildCardsdict);
                if (!similarWords.Any()) continue;

                foreach (var wordFound in similarWords)
                {
                    if (_dict[wordFound])
                        continue;

                    _dict[wordFound] = true;

                    Word newWordFound = new Word(wordFound, newWord);
                    queue.Enqueue(newWordFound);

                    if (wordFound.Equals(firstWord))
                    {
                        _target = newWordFound;
                        return;
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