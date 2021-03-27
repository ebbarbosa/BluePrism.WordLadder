using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    /// <summary>
    /// Wrapper for the System.IO.File class and its methods.
    /// </summary>
    public interface IFileWrapper
    {
        WordDictionaryStreamReader StreamReader(string fileName);
        void Write(IList<string> wordLadder, string filePath);
        bool FileExists(string fileName);
        bool IsValidPath(string fileName);
        bool HasTxtExtension(string fileName);
    }
}