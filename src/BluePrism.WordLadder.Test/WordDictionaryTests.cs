using BluePrism.WordLadder.Infrastructure;
using FluentAssertions;
using System.IO;
using Xunit;

namespace BluePrism.WordLadder.Test
{

    public class WordDictionaryTests
    {

        private readonly string _path;
        private readonly string _realFileName;

        public WordDictionaryTests()
        {
            _path = Directory.GetCurrentDirectory();
            _realFileName = $"{_path}//wordDict.txt";
        }

        private IWordDictionary CreateServiceUnderTest(string fileName)
        {
            return new WordDictionary(fileName);
        }

        [Fact]
        public void Constructor_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange& 
            string fileName = "/throwerror.txt";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => new WordDictionary(fileName));
        }


        [Fact]
        public void GetListOfWords_WhenFileExists_ReturnsListOfWords()
        {
            // Arrange 
            var sut = CreateServiceUnderTest(_realFileName);

            // Act
            var result = sut.GetListOfWords();

            // Assert
            result.Keys.Should().NotBeNull().And.NotBeEmpty();
        }

    }
}