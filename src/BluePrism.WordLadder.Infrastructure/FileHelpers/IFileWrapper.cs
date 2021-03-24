using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    public interface IFileWrapper
    {
        void Write(IList<string> wordLadder, string filePath);
    }
}