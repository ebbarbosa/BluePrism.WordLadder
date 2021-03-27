using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Extensions;

namespace BluePrism.WordLadder.Domain.Business
{
    public class GetSimilarWordsFromProcessedListService : IGetSimilarWordsFromProcessedListService
    {
        /// <summary>
        /// Get all possible wildcard transformations of <paramref name="word"/> to be added to the dictionary of <paramref name="preprocessedWords"/>.
        /// I.e. SAME gets *AME, S*ME, SA*E and SAM*.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="preprocessedWords">This dictionary will contain the key as the wildcard word and all the possible words it can transform to from the word dictionary provided. </param>
        /// <returns>Set with all adjacent words found so far for this <paramref name="word"/></returns>
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