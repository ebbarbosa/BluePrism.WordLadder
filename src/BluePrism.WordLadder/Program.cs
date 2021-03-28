using System;

namespace BluePrism.WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = Factory.CreateCommandLineWrapper();
            var argsResult = parser.GetResult(args);
            if (argsResult == null) return;

            var wordLadderApp = Factory.CreateWordLadderApp();
            wordLadderApp.Execute(argsResult, err => Console.Error.WriteLine(err));

            Console.Write("Press any <key> to exit... ");
            Console.ReadKey();
        }
    }
}
