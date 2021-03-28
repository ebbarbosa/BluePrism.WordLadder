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
        private readonly IOpenFileHelper _openFileHelper;
        private readonly IFileWrapper _fileWrapper;

        public WordLadderApp(IInputValidator inputValidator, IWordDictionaryService wordDictionaryService,
            IWordLadderSolver wordladderSolver, IOpenFileHelper openFileHelper, IFileWrapper fileWrapper)
        {
            _inputValidator = inputValidator;
            _wordDictionaryService = wordDictionaryService;
            _wordladderSolver = wordladderSolver;
            _openFileHelper = openFileHelper;
            _fileWrapper = fileWrapper;
        }

        public void Execute(Options args, Action<string> catchAction)
        {
            _inputValidator
                .Validate(args, ExecuteProgram)
                .HandleErrors(catchAction);
        }

        private void ExecuteProgram(Options argsResult)
        {
            _wordDictionaryService.Initialise(argsResult);

            var result = _wordladderSolver.SolveLadder(argsResult.StartWord,
                argsResult.EndWord,
                _wordDictionaryService.GetWordDictionary(),
                _wordDictionaryService.GetPreprocessedWordsDictionary());

            WriteResultToTxtFile(result, argsResult.WordLadderResultFilePath);
        }

        private void OpenFile(string wordLadderResultFilePath)
        {
            _openFileHelper.OpenFile(wordLadderResultFilePath);
        }

        private void WriteResultToTxtFile(IList<string> wordLadder, string filePath)
        {
            if (!wordLadder.Any())
            {
                Console.WriteLine(
                    ":( - Unfortunately, the word ladder returned no results. You may try again with different values.");
                return;
            }

            _fileWrapper.Write(wordLadder, filePath);
            OpenFile(filePath);
        }
    }
}