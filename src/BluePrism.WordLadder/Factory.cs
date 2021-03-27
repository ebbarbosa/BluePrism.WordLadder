using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;
using BluePrism.WordLadder.Infrastructure.Validators;

namespace BluePrism.WordLadder
{

    /// <summary>
    /// This class works as an IoC and DIContainer while no implementation is set. All dependencies are here so the other layers can de independent from implementation and use the interfaces only.
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

        public static ICommandLineWrapper CreateCommandLineWrapper()
        {
            return new CommandLineWrapper();
        }

        public static IInputValidator CreateInputValidator()
        {
            return new InputValidator(CreateFileWrapper());
        }
    }
}