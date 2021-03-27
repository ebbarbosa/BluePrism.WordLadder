using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain
{
    public interface IGetSimilarWordsFromProcessedListService
    {
        /// <summary>
        /// Get all possible wildcard transformations of <paramref name="word"/> to be added to the dictionary of <paramref name="preprocessedWords"/>.
        /// I.e. SAME gets *AME, S*ME, SA*E and SAM*.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="preprocessedWords">This dictionary will contain the key as the wildcard word and all the possible words it can transform to from the word dictionary provided. </param>
        /// <returns>Set with all adjacent words found so far for this <paramref name="word"/></returns>
        HashSet<string> GetSimiliarWords(string word, IDictionary<string, ICollection<string>> preprocessedWords);
    }
}