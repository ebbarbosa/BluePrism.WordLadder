using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BluePrism.WordLadder.Domain.Extensions;
using BluePrism.WordLadder.Domain.Models;

namespace BluePrism.WordLadder.Domain.Business
{
    public class WordLadderSolver : IWordLadderSolver
    {
        private readonly IGetSimilarWordsFromProcessedListService _getWordFromProcessedListService;

        /// <summary>
        /// root will hold the end word of the ladder and its parent.
        /// </summary>
        private Word _root;

        /// <summary>
        /// target holds the value of the start word to be found. Once target is found we have completed the ladder. If it is never found the ladder has no answer.
        /// </summary>
        private Word _target = null;

        /// <summary>
        /// This dictionary works as a memoizing object, to remeber the words already examined. It is initialised by <paramref name="wordDictionary"/>
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

        /// <summary>
        /// This method solves the word ladder. Using a BFS algorithm.
        /// </summary>
        /// <param name="firstWord">Start word for the word ladder</param>
        /// <param name="targetWord">End word for the word ladder</param>
        /// <param name="wordDictionary">Contains the words from the word list provided, filtered by alphabetical words only with the same lngth as the start word and boolean values set to false to indicate whether a word was already visited.</param>
        /// <param name="wordOfPreprocessedWords">Contains keys formed from wildcard transformations of the words from <paramref name="wordDictionary"/> and values are the words which can be the key possible transformations. I.e. key is C*ST the value will contain a colleciton of words such as COST, CAST, CIST.</param>
        /// <returns></returns>
        public IList<string> SolveLadder(string firstWord, string targetWord,
            IDictionary<string, bool> wordDictionary,
            IDictionary<string, ICollection<string>> wordOfPreprocessedWords)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            _root = new Word(targetWord);
            _dict = wordDictionary;
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
            }
        }
    }
}