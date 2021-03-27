using System.Collections.Generic;
using System.Text;

namespace BluePrism.WordLadder.Domain.Extensions
{

    public static class StringExtensions
    {

        /// <summary>
        /// This method creates all possible transformations for a word, replacing each letter with a wildcard char '*'.
        /// </summary>
        /// <param name="self">The string calling this function. A string with less than 2 chars long will generate a single string '*'.</param>
        /// <returns>All possible wildcard strings from the current string.</returns>
        public static IList<string> GetWildcardWords(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return new string[] { };
            var result = new List<string>();

            var newWord = self;
            for (int letterIndex = 0; letterIndex < self.Length; letterIndex++)
            {
                var wildCardString = new StringBuilder(newWord) { [letterIndex] = '*' };
                newWord = wildCardString.ToString();

                result.Add(newWord);
                newWord = self;
            }

            return result;
        }
    }
}