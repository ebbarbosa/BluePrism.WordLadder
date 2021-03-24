using System;
using System.Collections.Generic;
using System.IO;
using BluePrism.WordLadder.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class IntegrationTests
    {
        readonly IWordLadderSolver _sut;

        public IntegrationTests()
        {
            _sut = new WordLadderSolver(new GetSimilarWordsFromProcessedListService());
        }

        [Fact]
        public void SolveLadder_UsingRealDictionary_ReturnsTheShortestPath()
        {
            // Arrange
            var beginWord = "HIRE";
            var targetWord = "SORT";

            string path = Directory.GetCurrentDirectory();
            var fileName = $"{path}//content//words-english.txt";

            var wordDic = Factory.CreateWordDictionary(fileName, beginWord);

            var expectedResult = new List<string>() {"HIRE", "SIRE", "SORE", "SORT"};

            // Act
            var result = _sut.SolveLadder(beginWord, targetWord, wordDic.GetListOfWords(), wordDic.GetListOfPreprocessedWords());

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }
        
    }
}