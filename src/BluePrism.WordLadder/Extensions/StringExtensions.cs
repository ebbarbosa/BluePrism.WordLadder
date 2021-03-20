using System;
using System.Collections;
using System.Linq;

namespace BluePrism.WordLadder.Extensions
{

    public static class StringExtensions
    {

        /// <summary>
        /// This method compares two string contents and returns if a string has 
        ///
        /// </summary>
        /// <param name="self">The string calling this function. Must be at least 1 char long.</param>
        /// <param name="subject">String passed to be compared to self. Must be at least 1 char long.</param>
        /// <param name="numCharsToDiffer">How many chars must differ to return true.</param>
        /// <returns></returns>
        public static bool IsSimilarBy(this string self, string subject, int numCharsToDiffer)
        {

            if (string.IsNullOrWhiteSpace(self)) return false;
            if (string.IsNullOrWhiteSpace(subject)) return false;

            int count = 0;

            count = self.ToCharArray().Intersect(subject.ToCharArray()).Count();

            return self.Length - count == numCharsToDiffer;
        }

    }
}