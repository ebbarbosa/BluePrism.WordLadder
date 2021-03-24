using System;
using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain.Models
{
    public interface IGetSimilarWordsFromProcessedListService
    {
        HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords);
    }
}