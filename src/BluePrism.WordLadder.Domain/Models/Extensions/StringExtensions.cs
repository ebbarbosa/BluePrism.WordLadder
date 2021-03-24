using System.Collections.Generic;
using System.Text;

namespace BluePrism.WordLadder.Domain.Models.Extensions
{

    public static class StringExtensions
    {

        /// <summary>
        /// This method compares the current string to the specified subject string passed and uses an algorithm that iterates through each char of the current string while checking if the same letter is in the same index of the subject string. If the same index chars do match, it changes the letter for a wildcard char '*' on a temporary string, so when compared again it does not confound a repeated letter inside the string, and it also decrements the count initialised by the words length.
        /// </summary>
        /// <param name="self">The string calling this function. Must be at least 1 char long.</param>
        /// <param name="subject">String passed to be compared to self. Must be at least 1 char long.</param>
        /// <param name="numCharsToDiffer">How many chars must differ to return true.</param>
        /// <returns>In the end if the count is the same as the numCharsToDiffer parameter it returns true.</returns>
        public static bool IsDifferentOnlyBy(this string self, string subject, int numCharsToDiffer)
        {
            if (string.IsNullOrWhiteSpace(self)) return false;
            if (string.IsNullOrWhiteSpace(subject)) return false;

            if (self.Length != subject.Length)
                return false;

            var newWord = subject;
            var count = subject.Length;

            for (int letterIndex = 0; letterIndex < self.Length; letterIndex++)
            {
                var lookupLetter = self[letterIndex];
                if (letterIndex >= 0 && newWord[letterIndex].Equals(lookupLetter))
                {
                    var wildCardString = new StringBuilder(newWord) { [letterIndex] = '*' };
                    newWord = wildCardString.ToString();
                    count--;
                }
            }

            return count == numCharsToDiffer;
        }

        public static IEnumerable<string> GetPossibleWildcardMatches(this string self, IList<string> wordsToMatch)
        {
            var wildcards = self.GetWildcardWords();

            foreach (var wildcardWord in wildcards)
            {
                if (wordsToMatch.Contains(wildcardWord))
                    yield return wildcardWord;
            }
        }

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