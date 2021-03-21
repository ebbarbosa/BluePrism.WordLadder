using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Models;
using FluentAssertions;
using System.IO;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class FactoryTests
    {

        [Fact]
        public void CreateWordLadderSolver_ReturnsWordLadderClasseImplementationOfIWordLadder()
        {
            var result = Factory.CreateWordLadderSolver();

            result.Should().BeOfType<WordLadderSolver>().And.BeAssignableTo<IWordLadderSolver>();
        }

        [Fact]
        public void CreateFileWrapper_ReturnsFileWrapperClasseImplementationOfIFileWrapper()
        {
            var result = Factory.CreateFileWrapper();

            result.Should().BeOfType<FileWrapper>().And.BeAssignableTo<IFileWrapper>();
        }

        [Fact]
        public void CreateWordDictionary_ReturnsWordDictionaryClasseImplementationOfIWordDictionary()
        {
            string path = Directory.GetCurrentDirectory();
            var fileName = $"{path}//wordDict.txt";
            var result = Factory.CreateWordDictionary(fileName);

            result.Should().BeOfType<WordDictionary>().And.BeAssignableTo<IWordDictionary>();
        }

    }
}