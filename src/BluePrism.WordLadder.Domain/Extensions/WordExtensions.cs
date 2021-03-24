using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain.Models
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