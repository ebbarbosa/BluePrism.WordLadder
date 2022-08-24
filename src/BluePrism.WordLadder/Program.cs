using System;
using System.Reflection;
using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Infrastructure;
using Ninject;

namespace BluePrism.WordLadder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new StandardKernel();
            factory.Load(Assembly.GetExecutingAssembly());

            var parser = factory.Get<ICommandLineWrapper>();
            var argsResult = parser.GetResult(args);
            if (argsResult == null) return;

            var wordLadderApp = factory.Get<IWordLadderApp>();
            var result = wordLadderApp.GetResult(argsResult, err => Console.Error.WriteLine(err));

            var outputService = factory.Get<IOutputService>();
            outputService.Execute(result, argsResult);

            Console.Write("Press any <key> to exit... ");
            Console.ReadKey();
        }
    }
}