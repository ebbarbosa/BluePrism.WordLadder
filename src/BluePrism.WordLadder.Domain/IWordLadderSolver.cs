using System;
using System.Collections.Generic;

namespace BluePrism.WordLadder.Domain.Models
{
    public interface IWordLadderSolver
    {
        IList<string> SolveLadder(string beginWord,
            string targetWord,
            IDictionary<string, bool> wordDictionary,
            IDictionary<string, ICollection<string>> wordDicPreProcessed);
    }

}