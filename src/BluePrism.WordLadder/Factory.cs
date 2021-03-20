using BluePrism.WordLadder.Models;
using BluePrism.WordLadder.Infrastructure;

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
    }
}