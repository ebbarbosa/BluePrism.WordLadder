using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Models;

namespace BluePrism.WordLadder.Domain.Extensions
{
    public static class WordExtensions
    {
        
        /// <summary>
        /// This method gets the Word node and generates a list containing the <paramref name="word"/> value and all its parents WordKey values. Forming the word-ladder list. 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static IList<string> ToList(this Word word)
        {
            var result = new List<string>();
            var newWord = word;
            while (newWord != null)
            {
                result.Add(newWord.WordKey);
                newWord = newWord.ParentWord;
            }

            return result;
        }
    }
}