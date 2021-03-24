using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder
{

    public class Factory
    {
        public static IWordLadderSolver CreateWordLadderSolver()
        {
            return new WordLadderSolver(new GetSimilarWordsFromProcessedListService());
        }

        public static IWordDictionary CreateWordDictionary(string fileName, string source)
        {
            return new WordDictionary(fileName, source);
        }

        public static IFileWrapper CreateFileWrapper()
        {
            return new FileWrapper();
        }

        public static ICommandLineWrapper CreateCommandLineParser(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}