using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;

namespace BluePrism.WordLadder
{

    /// <summary>
    /// This class works as an IoC and DIContainer while no implementation is set.
    /// </summary>
    public class Factory
    {
        public static IWordLadderSolver CreateWordLadderSolver()
        {
            return new WordLadderSolver(new GetSimilarWordsFromProcessedListService());
        }

        public static IWordDictionaryService CreateWordDictionaryService()
        {
            return new WordDictionaryService(CreateFileWrapper(), new DictionaryPreprocessService());
        }

        public static IFileWrapper CreateFileWrapper()
        {
            return new FileWrapper();
        }

        public static ICommandLineWrapper CreateCommandLineWrapper(string[] args)
        {
            return new CommandLineWrapper(args);
        }
    }
}