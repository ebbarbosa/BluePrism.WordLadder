using System;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    /// <summary>
    /// This interface is used to get the program arguments and check if they are passed correctly.
    /// </summary>
    public interface ICommandLineWrapper
    {
        /// <summary>
        /// Gets the program parsed arguments.
        /// </summary>
        Options GetResult(string[] args);
    }
}