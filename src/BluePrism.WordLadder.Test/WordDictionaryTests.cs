using BluePrism.WordLadder.Infrastructure;
using FluentAssertions;
using System.IO;
using System.Linq;
using Xunit;

namespace BluePrism.WordLadder.Test
{

    public class WordDictionaryTests
    {
        private readonly string _realFileName;
        private readonly string _sourceWord;

        public WordDictionaryTests()
        {
            var path = Directory.GetCurrentDirectory();
            _realFileName = $"{path}//content//words-english.txt";
            _sourceWord = "TEst";
        }

        private IWordDictionary CreateServiceUnderTest(string fileName, string sourceWord)
        {
            return new WordDictionary(fileName, sourceWord);
        }

        [Fact]
        public void Constructor_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange& 
            string fileName = "/throwerror.txt";
            string sourceWord = "test";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => new WordDictionary(fileName, sourceWord));
        }

        [Fact]
        public void GetListOfWords_WhenFileExists_ReturnsListOfWords()
        {
            // Arrange 
            var sut = CreateServiceUnderTest(_realFileName,  _sourceWord);

            // Act
            var result = sut.GetListOfWords();

            // Assert
            result.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.ContainItemsAssignableTo<string>();

            foreach (var resultKey in result)
            {
                resultKey.Should().HaveLength(_sourceWord.Length);
            }

        }
    }
}