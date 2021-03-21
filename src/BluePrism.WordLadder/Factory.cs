using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Models;

namespace BluePrism.WordLadder
{

    public class Factory
    {
        public static IWordLadderSolver CreateWordLadderSolver()
        {
            return new WordLadderSolver();
        }

        public static IWordDictionary CreateWordDictionary(string fileName)
        {
            return new WordDictionary(fileName);
        }

        public static IFileWrapper CreateFileWrapper()
        {
            return new FileWrapper();
        }
    }
}