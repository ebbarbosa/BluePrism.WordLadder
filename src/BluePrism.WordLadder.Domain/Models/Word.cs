namespace BluePrism.WordLadder.Domain.Models
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
}