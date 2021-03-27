using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    /// <summary>
    /// Publicizes the methods to get the two dictionaries to be used as memoization objects in the word ladder solving process.
    /// The Word Dictionary and The Pre processed words dictionary.
    /// It needs to be initialised with the Options retrieved by the command line before use.
    /// </summary>
    public interface IWordDictionaryService
    {
        IDictionary<string, bool> GetWordDictionary();
        IDictionary<string, ICollection<string>> GetPreprocessedWordsDictionary();
        void Initialise(Options options);
    }
}