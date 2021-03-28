using System;
using System.Collections.Generic;
using System.IO;
using BluePrism.WordLadder.Domain.Models;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    /// <summary>
    /// Wrapper implementation for the System.IO.File class and its methods.
    /// </summary>
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

        public bool FileExists(string fileName)
        {
            var actualFileName = GetActualFilePath(fileName);
            return File.Exists(actualFileName);
        }

        private static string GetActualFilePath(string fileName)
        {
            var isPathRooted = Path.IsPathRooted(fileName);
            var actualFileName = isPathRooted ? fileName : Path.GetFullPath(fileName);
            return actualFileName;
        }

        public bool IsValidPath(string fileName)
        {
            var realPath = GetActualFilePath(fileName);
            var rc = Constants.Fail;
            try
            {
                using (new StreamWriter(realPath, true))
                {
                    rc = Constants.Pass;
                }
            }
            catch (Exception)
            {
                rc = Constants.Fail;
            }

            return rc;
        }

        public bool HasTxtExtension(string fileName)
        {
            return Path.HasExtension(fileName) && fileName.ToLower().EndsWith(".txt");
        }

        protected virtual void WriteAllLines(IList<string> wordLadder, string fileName)
        {
            File.WriteAllLines(fileName, wordLadder);
        }
    }
}