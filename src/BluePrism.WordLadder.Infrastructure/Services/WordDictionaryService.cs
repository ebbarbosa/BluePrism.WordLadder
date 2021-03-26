using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure.Services
{
    public class WordDictionaryService : IWordDictionaryService
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IDictionaryPreprocessService _dictionaryPreprocessService;
        private readonly IDictionary<string, bool> _listOfWords = new Dictionary<string, bool>() ;
        private readonly IDictionary<string, ICollection<string>> _listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();

        public WordDictionaryService(IFileWrapper fileWrapper, IDictionaryPreprocessService dictionaryPreprocessService)
        {
            _fileWrapper = fileWrapper;
            _dictionaryPreprocessService = dictionaryPreprocessService;
        }
        
        public void Initialise(string fileName, string sourceWord)
        {
            _listOfWords.Add(sourceWord, false);

            _fileWrapper.ValidateFile(fileName);
            
            using (var sr = _fileWrapper.StreamReader(fileName))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length != sourceWord.Length) continue;
                    _dictionaryPreprocessService.CreatePreprocessedDictionaries(new DictionaryPreprocessServiceParams(line, _listOfWords, _listOfPreprocessedWords));
                }
            }
        }

        public IDictionary<string, bool> GetWordDictionary()
        {
            return _listOfWords;
        }

        public IDictionary<string, ICollection<string>> GetPreprocessedWordsDictionary()
        {
            return _listOfPreprocessedWords;
        }
    }
}