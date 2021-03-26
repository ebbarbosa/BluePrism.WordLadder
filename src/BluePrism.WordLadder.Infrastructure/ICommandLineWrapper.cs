using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface ICommandLineWrapper
    {
        Options GetResult();
    }
}