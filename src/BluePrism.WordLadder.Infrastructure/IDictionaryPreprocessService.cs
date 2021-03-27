using BluePrism.WordLadder.Infrastructure.Services;

namespace BluePrism.WordLadder.Infrastructure
{
    /// <summary>
    /// Service that pre processes each line of the word pre processed dictionary, publicised by the Word Dictionary Service interface.
    /// </summary>
    public interface IDictionaryPreprocessService
    {
        void CreatePreprocessedDictionaries(
            DictionaryPreprocessServiceParams dictionaryPreprocessParams);
    }
}