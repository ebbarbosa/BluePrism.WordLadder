using System;
using System.Collections.Generic;
using System.Linq;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;

namespace BluePrism.WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = Factory.CreateCommandLineParser(args);
            parser.ValidateResult();
            var argsResult = parser.GetResult();

            ExecuteProgram(argsResult);

            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        private static void ExecuteProgram(Options argsResult)
        {
            var wordDic = Factory.CreateWordDictionary(argsResult.WordDictionaryFilePath, argsResult.BeginWord);
            var wordladderSolver = Factory.CreateWordLadderSolver();

            var result = wordladderSolver.SolveLadder(argsResult.BeginWord,
                argsResult.TargetWord,
                wordDic.GetWordDictionary(),
                wordDic.GetListOfPreprocessedWords());

            WriteResultToTxtFile(result, argsResult.WordLadderResultFilePath);
        }

        static void WriteResultToTxtFile(IList<string> wordLadder, string filePath)
        {
            if (!wordLadder.Any()) Console.WriteLine(":( - Unfortunately, the word ladder returned no results. You may try again with different values.");

            var fileWrapper = Factory.CreateFileWrapper();
            fileWrapper.Write(wordLadder, filePath);
        }
    }
}
