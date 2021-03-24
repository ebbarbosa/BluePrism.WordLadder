using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class FileWrapperTester : FileWrapper
    {
        public IList<string> WordLadder { get; private set; }

        public string FileName { get; private set; }

        protected override void WriteAllLines(IList<string> wordLadder, string fileName)
        {
            WordLadder = wordLadder;
            FileName = fileName;
        }
    }
}