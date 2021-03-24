using System;
using System.Collections.Generic;
using System.Linq;
using BluePrism.WordLadder.Domain.Models.Extensions;

namespace BluePrism.WordLadder.Domain.Models
{
    public class GetSimilarWordsFromProcessedListService : IGetSimilarWordsFromProcessedListService
    {
        public HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords)
        {
            var wildcardWords = word.GetWildcardWords();
            var words = new HashSet<string>();

            foreach (var wildcardWord in wildcardWords)
            {
                var nodesFound = preprocessedWords[wildcardWord];
                words.UnionWith(nodesFound);
            }
            
            return words;
        }
    }
}