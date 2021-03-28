using System;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Application
{
    public interface IWordLadderApp
    {
        void Execute(Options args, Action<string> catchAction);
    }
}