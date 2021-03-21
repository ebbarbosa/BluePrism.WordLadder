using System.Collections.Generic;

namespace BluePrism.WordLadder.Models
{
    public class Word
    {
        public string WordKey { get; private set; }
        public Word ParentWord { get; private set; }

        public Word(string wordKey) : this(wordKey, null) { }
        public Word(string wordKey, Word parentWord)
        {
            WordKey = wordKey;
            ParentWord = parentWord;
        }
    }

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