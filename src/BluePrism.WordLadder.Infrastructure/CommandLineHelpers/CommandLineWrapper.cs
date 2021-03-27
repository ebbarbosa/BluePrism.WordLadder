using System;
using System.Collections.Generic;
using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    /// <summary>
    /// This class uses command line parser to check if the program arguments are passed correctly.
    /// </summary>
    public class CommandLineWrapper : ICommandLineWrapper
    {
        private Options _resultArgs;

        /// <summary>
        /// Gets the program parsed arguments. If arguments are not parsed writes the help to the console.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Options GetResult(string[] args)
        {
            var parser = new Parser(config =>
            {
                config.HelpWriter = Console.Out;
                config.AutoVersion = false;
            });
            var parserResult = parser.ParseArguments<Options>(args);
            parserResult
                .WithParsed(HandleOptions)
                .WithNotParsed(HandleErrors);

            return _resultArgs;
        }

        private void HandleErrors(IEnumerable<Error> obj)
        {
            obj.Output();
        }

        private void HandleOptions(Options opt)
        {
            _resultArgs = opt;
        }
    }
}