using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{

    public class WordDictionary : IWordDictionary
    {
        private IDictionary<string, bool> _listOfWords;

        public WordDictionary(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) throw new FileNotFoundException("Dictionary file not found", fileName);
                _listOfWords = new Dictionary<string, bool>();
                using (var sr = new StreamReader(fileName))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        _listOfWords.Add(line, false);
                    }
                }
            }
            catch (System.Exception)
            {
                // todo: add logger
                throw;
            }
            finally // TODO: REMOVE THIS TEST CODE
            {
                _listOfWords = new Dictionary<string, bool>
                 {  { "HARD", false },
                    { "DART", false },
                    { "GORE", false },
                    { "GARD", false },
                    { "TARD", false },
                    { "DRED", false },
                    { "FRED", false },
                    { "BRED", false },
                    { "DARE", false },
                    { "FORK", false },
                    { "FARE", false },
                    { "BEAR", false },
                    { "BORE", false }
                };
            }
        }

        public IDictionary<string, bool> GetListOfWords()
        {
            return _listOfWords;
        }
    }
}