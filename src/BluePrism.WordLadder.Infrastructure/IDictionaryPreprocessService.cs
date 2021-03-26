using BluePrism.WordLadder.Infrastructure.Services;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IDictionaryPreprocessService
    {
        void CreatePreprocessedDictionaries(
            DictionaryPreprocessServiceParams dictionaryPreprocessParams);
    }
}