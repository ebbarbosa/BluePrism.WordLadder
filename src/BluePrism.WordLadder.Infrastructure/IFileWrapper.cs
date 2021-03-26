using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure.FileHelpers;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IFileWrapper
    {
        WordDictionaryStreamReader StreamReader(string fileName);
        void Write(IList<string> wordLadder, string filePath);
        void ValidateFile(string fileName);
    }
}