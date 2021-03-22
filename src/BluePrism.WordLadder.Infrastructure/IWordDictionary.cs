using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IWordDictionary
    {
        HashSet<string> GetListOfWords();
    }
}