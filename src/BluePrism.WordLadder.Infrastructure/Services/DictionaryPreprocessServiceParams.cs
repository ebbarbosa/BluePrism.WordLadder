using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure.Services
{
    public class DictionaryPreprocessServiceParams
    {
        public string Line { get; }
        public IDictionary<string, bool> ListOfWords { get; }
        public IDictionary<string, ICollection<string>> ListOfPreprocessedWords { get; }

        public DictionaryPreprocessServiceParams(string line, IDictionary<string, bool> listOfWords,
            IDictionary<string, ICollection<string>> listOfPreprocessedWords)
        {
            Line = line;
            ListOfWords = listOfWords;
            ListOfPreprocessedWords = listOfPreprocessedWords;
        }
    }
}