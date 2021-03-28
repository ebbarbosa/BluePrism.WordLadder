using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Extensions;

namespace BluePrism.WordLadder.Infrastructure.Services
{
    public class DictionaryPreprocessService : IDictionaryPreprocessService
    {
        /// <summary>
        /// This method checks if the line being read is contained in the word list and adds it.
        /// It also adds the line word to all possible entries in the preprocessed dictionary or creates new ones using the GetWildcardWords() to define the pre processed keys.
        /// </summary>
        /// <param name="dictionaryPreprocessParams">Holds the line being read from the dictionaary file, the list of words and the list of pre processed words.</param>
        public void CreatePreprocessedDictionaries(DictionaryPreprocessServiceParams dictionaryPreprocessParams)
        {
            var line = dictionaryPreprocessParams.Line;
            var listOfWords = dictionaryPreprocessParams.ListOfWords;
            var listOfPreprocessedWords = dictionaryPreprocessParams.ListOfPreprocessedWords;

            if (!listOfWords.ContainsKey(line))
                listOfWords.Add(line, false);

            var preprocessedWords = line.GetWildcardWords();
            foreach (var preprocessedWord in preprocessedWords)
                if (listOfPreprocessedWords.ContainsKey(preprocessedWord))
                    listOfPreprocessedWords[preprocessedWord].Add(line);
                else
                    listOfPreprocessedWords.Add(preprocessedWord, new List<string> {line});
        }
    }
}