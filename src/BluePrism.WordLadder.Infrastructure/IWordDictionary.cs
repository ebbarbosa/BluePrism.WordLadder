using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IWordDictionary
    {
        IDictionary<string, bool> GetWordDictionary();
        IDictionary<string, ICollection<string>> GetListOfPreprocessedWords();
    }
}