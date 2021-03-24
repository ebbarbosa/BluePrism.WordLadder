using System;
using System.Collections.Generic;
using System.IO;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    public class FileWrapper : IFileWrapper
    {
        public void Write(IList<string> wordLadder, string fileName)
        {
            try
            {
                WriteAllLines(wordLadder, fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        protected virtual void WriteAllLines(IList<string> wordLadder, string fileName)
        {
            File.WriteAllLines(fileName, wordLadder);
        }
    }
}
