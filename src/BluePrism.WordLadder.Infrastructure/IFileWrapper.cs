using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public interface IFileWrapper
    {
        void Write(IList<string> wordLadder, string filePath);
    }
}