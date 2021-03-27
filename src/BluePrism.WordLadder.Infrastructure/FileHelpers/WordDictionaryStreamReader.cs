using System.IO;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    /// <summary>
    /// Custom Implementation of a stream reader to pre process values for the word ladder dictionary of words.
    /// </summary>
    public class WordDictionaryStreamReader : StreamReader
    {

        public WordDictionaryStreamReader(string fileName) : base(fileName)
        {
        }

        public WordDictionaryStreamReader(MemoryStream memoryStream) : base(memoryStream)
        {
        }

        /// <summary>
        /// Overides the read line method to remove numerical characters from words coming from the word dictionary file provided.
        /// This removal will be filtered later in the WordDictionaryService class.
        /// </summary>
        /// <returns></returns>
        public override string ReadLine()
        {
            var line = base.ReadLine();
            if (string.IsNullOrEmpty(line)) return line;

            var newLine = System.Text.RegularExpressions.Regex.Replace(line, @"[^a-zA-Z\s]", string.Empty);
            return newLine.ToUpperInvariant();
        }
    }
}