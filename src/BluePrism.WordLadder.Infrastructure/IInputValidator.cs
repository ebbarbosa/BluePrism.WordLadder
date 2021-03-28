using System;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    /// <summary>
    /// Validates the program input arguments and publicizes a continuation action and an error handler action to the caller.
    /// </summary>
    public interface IInputValidator
    {
        IInputValidator Validate(Options inputArgs, Action<Options> executeProgram);

        void HandleErrors(Action<string> catchAction);
    }
}