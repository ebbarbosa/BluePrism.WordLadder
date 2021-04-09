using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IOutputService
    {
        void Execute(IList<string> result, Options options);
    }
}