using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain
{
    public interface IGetSimilarWordsFromProcessedListService
    {
        HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords);
    }
}