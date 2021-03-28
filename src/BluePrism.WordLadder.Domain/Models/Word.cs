namespace BluePrism.WordLadder.Domain.Models
{
    /// <summary>
    /// Class to represent a word node on our BFS algorithm.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// The actual word node.
        /// </summary>
        public string WordKey { get; private set; }

        /// <summary>
        /// The parent word node for this instance.
        /// </summary>
        public Word ParentWord { get; private set; }

        public Word(string wordKey) : this(wordKey, null)
        {
        }

        public Word(string wordKey, Word parentWord)
        {
            WordKey = wordKey;
            ParentWord = parentWord;
        }
    }
}