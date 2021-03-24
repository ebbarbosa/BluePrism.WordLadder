using System;
using System.Collections.Generic;
using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    public class CommandLineWrapper : ICommandLineWrapper
    {
        private readonly ParserResult<Options> _resultParsing;
        private Options _resultArgs;

        public CommandLineWrapper(string[] args)
        {
            // (1) default options
            _resultParsing = Parser.Default.ParseArguments<Options>(args);
        }

        public void ValidateResult()
        {
            _resultParsing
                .WithParsed(HandleOptions)
                .WithNotParsed(HandleErrors);
        }

        private void HandleOptions(Options opt)
        {
            Console.WriteLine(opt.BeginWord);
        }

        private void HandleErrors(IEnumerable<Error> obj)
        {
            Console.WriteLine(obj.Output());
        }

        public Options GetResult()
        {
            _resultParsing.WithParsed(opt =>
            {
                _resultArgs = opt;
            });
            return _resultArgs;
        }
    }
}