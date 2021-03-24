using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure;
using FluentAssertions;
using System.IO;
using Xunit;

namespace BluePrism.WordLadder.Test
{

    public class WordDictionaryTests
    {
        private readonly string _sourceWord;
        private IWordDictionary _sut;
        private readonly string _realFileName;

        public WordDictionaryTests()
        {
            var path = Directory.GetCurrentDirectory();
            _realFileName = $"{path}//content//words-english.txt";
            _sourceWord = "TEst";
        }

        private static IWordDictionary CreateServiceUnderTest(string fileName, string sourceWord)
        {
            return new WordDictionary(fileName, sourceWord);
        }

        [Fact]
        public void Constructor_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange 
            string fileName = "/throwerror.txt";
            string sourceWord = "test";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => new WordDictionary(fileName, sourceWord));
        }

        [Fact]
        public void GetListOfWords_WhenFileExists_ReturnsListOfWords()
        {
            // Arrange
            _sut = CreateServiceUnderTest(_realFileName, _sourceWord);

            // Act
            var result = _sut.GetListOfWords();

            // Assert
            result.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.Subject.Keys.Should().AllBeAssignableTo<string>();
            result.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.Subject.Values.Should().AllBeAssignableTo<bool>();

            foreach (var resultKey in result.Keys)
            {
                resultKey.Should().HaveLength(_sourceWord.Length);
            }
        }

        [Fact]
        public void GetListOfPreprocessedWords_WhenFileExists_ReturnsTuplesListOfWildCardedWordsAndTheirIndexes()
        {
            // Arrange
            _sut = CreateServiceUnderTest(_realFileName, _sourceWord);

            // Act
            var result = _sut.GetListOfPreprocessedWords();

            // Assert
            result.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.Subject.Keys.Should().ContainItemsAssignableTo<string>();

            result.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.Subject.Values.Should().ContainItemsAssignableTo<IEnumerable<string>>();
        }
    }
}