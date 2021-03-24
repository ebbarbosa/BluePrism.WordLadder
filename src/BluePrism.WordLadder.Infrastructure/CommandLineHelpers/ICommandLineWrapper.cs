namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    public interface ICommandLineWrapper
    {
        void ValidateResult();
        Options GetResult();
    }
}