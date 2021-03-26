using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Extensions;

namespace BluePrism.WordLadder.Infrastructure.Services
{
    public class DictionaryPreprocessService : IDictionaryPreprocessService
    {
        public void CreatePreprocessedDictionaries(DictionaryPreprocessServiceParams dictionaryPreprocessParams)
        {
            var line = dictionaryPreprocessParams.Line;
            var listOfWords = dictionaryPreprocessParams.ListOfWords;
            var listOfPreprocessedWords = dictionaryPreprocessParams.ListOfPreprocessedWords;
            
            if (!listOfWords.ContainsKey(line))
                listOfWords.Add(line, false);

            var preprocessedWords = line.GetWildcardWords();
            foreach (var preprocessedWord in preprocessedWords)
            {
                if (listOfPreprocessedWords.ContainsKey(preprocessedWord))
                    listOfPreprocessedWords[preprocessedWord].Add(line);
                else
                    listOfPreprocessedWords.Add(preprocessedWord, new List<string> { line });
            }
        }
    }
}