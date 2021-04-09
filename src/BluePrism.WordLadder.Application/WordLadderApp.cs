using System;
using System.Collections.Generic;
using System.Linq;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder.Application
{
    public class WordLadderApp : IWordLadderApp
    {
        private readonly IInputValidator _inputValidator;
        private readonly IWordDictionaryService _wordDictionaryService;
        private readonly IWordLadderSolver _wordladderSolver;

        public WordLadderApp(IInputValidator inputValidator, IWordDictionaryService wordDictionaryService,
            IWordLadderSolver wordladderSolver)
        {
            _inputValidator = inputValidator;
            _wordDictionaryService = wordDictionaryService;
            _wordladderSolver = wordladderSolver;
        }

        public IList<string> GetResult(Options args, Action<string> catchAction)
        {
            IList<string> result = new List<string>();

            _inputValidator
                .Validate(args, opt => result = ExecuteProgram(opt))
                .HandleErrors(catchAction);

            return result;
        }

        private IList<string> ExecuteProgram(Options argsResult)
        {
            _wordDictionaryService.Initialise(argsResult);

            var result = _wordladderSolver.SolveLadder(argsResult.StartWord,
                argsResult.EndWord,
                _wordDictionaryService.GetWordDictionary(),
                _wordDictionaryService.GetPreprocessedWordsDictionary());

            return result;
        }
    }
}