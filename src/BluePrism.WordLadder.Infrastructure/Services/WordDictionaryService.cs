using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure.Services
{
    /// <summary>
    /// This class reads the words dictionary file and creates two dictionaries to be used as memoization objects in the word ladder solving process.
    /// </summary>
    public class WordDictionaryService : IWordDictionaryService
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IDictionaryPreprocessService _dictionaryPreprocessService;
        private readonly IDictionary<string, bool> _listOfWords = new Dictionary<string, bool>();
        private readonly IDictionary<string, ICollection<string>> _listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();

        public WordDictionaryService(IFileWrapper fileWrapper, IDictionaryPreprocessService dictionaryPreprocessService)
        {
            _fileWrapper = fileWrapper;
            _dictionaryPreprocessService = dictionaryPreprocessService;
        }

        public void Initialise(Options options)
        {
            InitialiseDictionaries(options);

            _fileWrapper.FileExists(options.WordDictionaryFilePath);

            using (var sr = _fileWrapper.StreamReader(options.WordDictionaryFilePath))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length != options.StartWord.Length) continue;
                    _dictionaryPreprocessService.CreatePreprocessedDictionaries(new DictionaryPreprocessServiceParams(line, _listOfWords, _listOfPreprocessedWords));
                }
            }
        }

        private void InitialiseDictionaries(Options options)
        {
            _listOfWords.Add(options.StartWord, false);
            _dictionaryPreprocessService.CreatePreprocessedDictionaries(
                new DictionaryPreprocessServiceParams(options.StartWord, _listOfWords, _listOfPreprocessedWords));
            _listOfWords.Add(options.EndWord, false);
            _dictionaryPreprocessService.CreatePreprocessedDictionaries(
                new DictionaryPreprocessServiceParams(options.EndWord, _listOfWords, _listOfPreprocessedWords));
        }

        /// <summary>
        /// Gets the word dictionary initialised with false in all values. 
        /// </summary>
        /// <returns>All possible words for the word ladder</returns>
        public IDictionary<string, bool> GetWordDictionary()
        {
            return _listOfWords;
        }

        /// <summary>
        /// Gets the Pre processed word dictionary.
        /// </summary>
        /// <returns>Dictionary containing all possible transformation keys and their adjacent words.</returns>
        public IDictionary<string, ICollection<string>> GetPreprocessedWordsDictionary()
        {
            return _listOfPreprocessedWords;
        }
    }
}