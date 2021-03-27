using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain
{
    public interface IWordLadderSolver
    {
        /// <summary>
        /// This method solves the word ladder. Using a BFS algorithm.
        /// </summary>
        /// <param name="firstWord">Start word for the word ladder</param>
        /// <param name="targetWord">End word for the word ladder</param>
        /// <param name="wordDictionary">Contains the words from the word list provided, filtered by alphabetical words only with the same lngth as the start word and boolean values set to false to indicate whether a word was already visited.</param>
        /// <param name="wordOfPreprocessedWords">Contains keys formed from wildcard transformations of the words from <paramref name="wordDictionary"/> and values are the words which can be the key possible transformations. I.e. key is C*ST the value will contain a colleciton of words such as COST, CAST, CIST.</param>
        /// <returns></returns>
        IList<string> SolveLadder(string beginWord,
            string targetWord,
            IDictionary<string, bool> wordDictionary,
            IDictionary<string, ICollection<string>> preprocessedWordsDictionary);
    }

}