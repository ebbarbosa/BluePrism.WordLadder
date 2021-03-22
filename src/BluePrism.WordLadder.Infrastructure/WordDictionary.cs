using System.Collections.Generic;
using System.IO;

namespace BluePrism.WordLadder.Infrastructure
{

    public class WordDictionary : IWordDictionary
    {
        private readonly HashSet<string> _listOfWords;

        public WordDictionary(string fileName, string sourceWord)
        {
            try
            {
                if (!File.Exists(fileName)) throw new FileNotFoundException("Dictionary file not found", fileName);
                _listOfWords = new HashSet<string>();
                using (var sr = new WordDictionaryStreamReader(fileName))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length == sourceWord.Length)
                            _listOfWords.Add(line);
                    }
                }
            }
            catch (System.Exception)
            {
                // todo: add logger
                throw;
            }
        }

        public HashSet<string> GetListOfWords()
        {
            return _listOfWords;
        }
    }
}