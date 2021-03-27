using System;
using System.Collections.Generic;
using System.Linq;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = Factory.CreateCommandLineWrapper();
            var argsResult = parser.GetResult(args);
            if (argsResult == null) return;

            var inputValidator = Factory.CreateInputValidator();
            inputValidator
                .Validate(argsResult, ExecuteProgram)
                .HandleErrors(Console.Error.WriteLine);

            Console.Write("Press any <key> to exit... ");
            Console.ReadKey();
        }

        static void ExecuteProgram(Options argsResult)
        {
            var wordDictionaryService = Factory.CreateWordDictionaryService();
            wordDictionaryService.Initialise(argsResult);

            var wordladderSolver = Factory.CreateWordLadderSolver();
            var result = wordladderSolver.SolveLadder(argsResult.StartWord,
                argsResult.EndWord,
                wordDictionaryService.GetWordDictionary(),
                wordDictionaryService.GetPreprocessedWordsDictionary());

            WriteResultToTxtFile(result, argsResult.WordLadderResultFilePath);
            OpenFile(argsResult.WordLadderResultFilePath);
        }

        static void OpenFile(string wordLadderResultFilePath)
        {
            var fileName = @$"file:///{wordLadderResultFilePath}";
            OpenFileHelper.OpenFile(fileName);
        }

        static void WriteResultToTxtFile(IList<string> wordLadder, string filePath)
        {
            if (!wordLadder.Any())
            {
                Console.WriteLine(":( - Unfortunately, the word ladder returned no results. You may try again with different values.");
                return;
            }

            var fileWrapper = Factory.CreateFileWrapper();
            fileWrapper.Write(wordLadder, filePath);
        }
    }
}
