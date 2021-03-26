using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Extensions;

namespace BluePrism.WordLadder.Domain.Business
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