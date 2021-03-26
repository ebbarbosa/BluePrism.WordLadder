using System.IO;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    public class WordDictionaryStreamReader : StreamReader
    {

        public WordDictionaryStreamReader(string fileName)
            : base(fileName)
        {
        }

        public override string ReadLine()
        {
            var line = base.ReadLine();
            if (string.IsNullOrEmpty(line)) return line;

            var newLine = System.Text.RegularExpressions.Regex.Replace(line, @"[^a-zA-Z\s]", string.Empty);
            return newLine.ToUpperInvariant();
        }
    }
}