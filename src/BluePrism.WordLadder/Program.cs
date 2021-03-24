using System;
using System.Collections.Generic;

namespace BluePrism.WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            // todo: create a model and a validator for the args, this should log the exception and a help response to the console.
            /*
             * something like:
             *
             * var argsBuilder = Factory.CreateArgsBuilder();
             *
             * var args = argsBuilder.AWord(args[0]).AWord(args[1]).AFilePath(args[2]).AFilePath(args[3])
             *  .Build()
             *  .LogsWith(Console.WriteLine)
             *  .Validate()
             * 
             */

            string beginWord = args[0];
            string targetWord = args[1];
            string wordDicFilePath = args[2];
            string resultTxtFilePath = args[3];

            var wordDic = Factory.CreateWordDictionary(wordDicFilePath, beginWord);
            var wordladderSolver = Factory.CreateWordLadderSolver();

            var result = wordladderSolver.SolveLadder(beginWord, 
                                                                targetWord, 
                                                                wordDic.GetListOfWords(), 
                                                                wordDic.GetListOfPreprocessedWords());

            WriteResultToTxtFile(result, resultTxtFilePath);

            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        static void WriteResultToTxtFile(IList<string> wordLadder, string filePath)
        {
            var fileWrapper = Factory.CreateFileWrapper();
            fileWrapper.Write(wordLadder, filePath);
        }
    }
}
