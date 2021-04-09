using System;
using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Application
{
    public interface IWordLadderApp
    {
        IList<string> GetResult(Options args, Action<string> catchAction);
    }
}