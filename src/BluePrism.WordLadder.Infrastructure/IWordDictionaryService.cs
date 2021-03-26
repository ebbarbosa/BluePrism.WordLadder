using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IWordDictionaryService
    {
        IDictionary<string, bool> GetWordDictionary();
        IDictionary<string, ICollection<string>> GetPreprocessedWordsDictionary();
        void Initialise(string fileName, string sourceWord);
    }
}