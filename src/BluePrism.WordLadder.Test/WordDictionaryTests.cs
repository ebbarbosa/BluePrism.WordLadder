using Xunit;
using FluentAssertions;
using System;
using System.IO;
using BluePrism.WordLadder.Infrastructure;

namespace BluePrism.WordLadder.Test
{

    public class WordDictionaryTests
    {
    
        private WordDictionary _sut;

        public WordDictionaryTests()
        {
            string path = Directory.GetCurrentDirectory();
            var fileName = $"{path}//wordDict.txt";

            _sut = new WordDictionary(fileName);
        }

        [Fact]
        public void GetListOfWords_WhenFileExists_ReturnsListOfWords()
        {
            // Act
            var result = _sut.GetListOfWords();

            // Assert
            result.Keys.Should().NotBeNull().And.NotBeEmpty();
        }

    }
}