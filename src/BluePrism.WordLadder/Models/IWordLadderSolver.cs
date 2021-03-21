using System.Collections.Generic;

namespace BluePrism.WordLadder.Models
{
    public interface IWordLadderSolver
    {
        IList<string> SolveLadder(string firstWord,
                                  string targetWord,
                                  IDictionary<string, bool> wordDictionary);
    }

}