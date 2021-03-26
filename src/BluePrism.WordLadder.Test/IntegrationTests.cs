using System;
using System.Collections.Generic;
using System.IO;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
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

            var wordDicService = Factory.CreateWordDictionaryService();
            wordDicService.Initialise(fileName, beginWord);

            var expectedResult = new List<string>() {"HIRE", "SIRE", "SORE", "SORT"};

            // Act
            var result = _sut.SolveLadder(beginWord, targetWord, wordDicService.GetWordDictionary(), wordDicService.GetPreprocessedWordsDictionary());

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }
        
    }
}