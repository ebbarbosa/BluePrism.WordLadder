using System;
using System.Collections.Generic;
using System.IO;

namespace BluePrism.WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {

            var firstWord = args[0];
            var lastWord = args[1];
            var wordDicFilePath = args[2];
            var resultTxtFilePath = args[3];

            var wordDic = Factory.CreateWordDictionary(wordDicFilePath);
            
            var wordladderSolver = Factory.CreateWordLadderSolver();
            var result = wordladderSolver.SolveLadder(firstWord, lastWord, wordDic.GetListOfWords());

            WriteResultToTxtFile(result, resultTxtFilePath);
            
            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) {}
        }

        static void WriteResultToTxtFile(IList<string> wordLadder, string filePath){
            Console.WriteLine(wordLadder);
        }
    }
}
