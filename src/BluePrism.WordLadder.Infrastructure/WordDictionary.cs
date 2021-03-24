using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BluePrism.WordLadder.Domain.Models.Extensions;
using NSubstitute;

namespace BluePrism.WordLadder.Infrastructure
{

    public class WordDictionary : IWordDictionary
    {
        private readonly IDictionary<string, bool> _listOfWords;
        private readonly IDictionary<string, ICollection<string>> _listOfPreprocessedWords;

        public WordDictionary(string fileName, string sourceWord)
        {
            _listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();
            _listOfWords = new Dictionary<string, bool>();

            try
            {
                if (!File.Exists(fileName)) throw new FileNotFoundException("Dictionary file not found", fileName);

                using (var sr = new WordDictionaryStreamReader(fileName))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length == sourceWord.Length)
                        {
                            if (_listOfWords.ContainsKey(line)) continue;
                            _listOfWords.Add(line, false);

                            var preprocessedWords = line.GetWildcardWords();
                            foreach (var preprocessedWord in preprocessedWords)
                            {
                                if (_listOfPreprocessedWords.ContainsKey(preprocessedWord))
                                    _listOfPreprocessedWords[preprocessedWord].Add(line);
                                else
                                    _listOfPreprocessedWords.Add(preprocessedWord, new List<string> { line });
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                // todo: add logger
                throw;
            }
        }

        public IDictionary<string, bool> GetListOfWords()
        {
            return _listOfWords;
        }

        public IDictionary<string, ICollection<string>> GetListOfPreprocessedWords()
        {
            return _listOfPreprocessedWords;
        }
    }
}