using System;
using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IWordDictionary
    {
        IDictionary<string, bool> GetListOfWords();
        IDictionary<string, ICollection<string>> GetListOfPreprocessedWords();
    }
}