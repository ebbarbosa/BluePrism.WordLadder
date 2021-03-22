using System.Text;

namespace BluePrism.WordLadder.Models.Extensions
{

    public static class StringExtensions
    {

        /// <summary>
        /// This method compares two string contents and returns if a string has 
        /// only the number of chars different from the subject string.
        /// This algorithm iterates through the chars of the caller string (self) while checking if the same letter is in the same index of the subject string copy newWord.
        /// If they match, it changes the letter on the newWord string for a wildcard char '*', so when compared again it does not confound a repeated letter, and it also decrements the count, initialised by the words length.
        /// In the end if the count is the same as the num of Chars to differ it returns true.
        /// </summary>
        /// <param name="self">The string calling this function. Must be at least 1 char long.</param>
        /// <param name="subject">String passed to be compared to self. Must be at least 1 char long.</param>
        /// <param name="numCharsToDiffer">How many chars must differ to return true.</param>
        /// <returns></returns>
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
                    var wildCardString = new StringBuilder(newWord) {[letterIndex] = '*'};
                    newWord = wildCardString.ToString();
                    count--;
                }
            }

            return count == numCharsToDiffer;
        }

    }
}