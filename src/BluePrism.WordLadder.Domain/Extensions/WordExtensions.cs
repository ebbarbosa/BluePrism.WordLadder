using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Models;

namespace BluePrism.WordLadder.Domain.Extensions
{
    public static class WordExtensions
    {
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