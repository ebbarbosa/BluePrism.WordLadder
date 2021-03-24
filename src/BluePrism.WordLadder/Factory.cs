using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure;

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
    }
}