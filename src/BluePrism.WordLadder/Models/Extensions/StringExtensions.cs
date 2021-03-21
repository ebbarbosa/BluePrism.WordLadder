namespace BluePrism.WordLadder.Models.Extensions
{

    public static class StringExtensions
    {

        /// <summary>
        /// This method compares two string contents and returns if a string has 
        /// only the number of chars different from the subject string.
        /// This algo uses a tabulating strategy to check the similar letters. Removing the letters that are in the compared string and in the end counting how many are left.
        /// If the length is the same as the num of Chars to differ this returns true.
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

            string newWord = subject;

            foreach (var lookupLetter in self)
            {
                var letterIndex = newWord.IndexOf(lookupLetter);
                if (letterIndex >= 0)
                {
                    newWord = newWord.Remove(letterIndex, 1);
                }
            }

            return newWord.Length == numCharsToDiffer;
        }

    }
}