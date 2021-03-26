using System;
using System.Collections.Generic;
using System.IO;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    public class FileWrapper : IFileWrapper
    {
        public WordDictionaryStreamReader StreamReader(string fileName)
        {
            return new WordDictionaryStreamReader(fileName);
        }

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

        public void ValidateFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("Word Dictionary file not found", fileName);
        }

        public void ValidateFilePath(string fileName)
        {
            if (!Directory.Exists(fileName))
            {
                Console.WriteLine("The answer file will be created on the root folder of the application...");
                File.Create(fileName);
            }
        }

        protected virtual void WriteAllLines(IList<string> wordLadder, string fileName)
        {
            File.WriteAllLines(fileName, wordLadder);
        }
    }
}
