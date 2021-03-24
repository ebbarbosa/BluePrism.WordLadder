using System;
using System.Collections.Generic;
using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    public class CommandLineWrapper : ICommandLineWrapper
    {
        private readonly Parser _parser;
        private readonly string[] _args;
        private ParserResult<Options> _resultParsing;
        private Options _resultArgs;

        public CommandLineWrapper(string[] args)
        {
            _parser = new Parser(config => config.HelpWriter = Console.Out);
            _args = args;
        }

        public Options GetResult()
        {
            _resultParsing = _parser.ParseArguments<Options>(_args);
            _resultParsing
                .WithParsed(HandleOptions)
                .WithNotParsed(HandleErrors);

            return _resultArgs;
        }

        private void HandleOptions(Options opt)
        {
            _resultArgs = opt;
        }

        private void HandleErrors(IEnumerable<Error> obj)
        {
            obj.Output();
        }
    }
}