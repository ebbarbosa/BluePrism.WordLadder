using System;
using System.Collections.Generic;
using System.IO;
using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
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
            var beginWord = "SATE";
            var targetWord = "COST";

            string path = Directory.GetCurrentDirectory();
            var fileName = $"{path}//content//words-english.txt";

            var answerFile = $"{path}//answer.txt";

            var wordDicService = Factory.CreateWordDictionaryService();
            wordDicService.Initialise(new Options(beginWord, targetWord, fileName, answerFile));

            var expectedResult = new List<string>() { "SATE", "LATE", "LASE", "LAST", "LOST", "COST" };

            // Act
            var result = _sut.SolveLadder(beginWord, targetWord, wordDicService.GetWordDictionary(), wordDicService.GetPreprocessedWordsDictionary());

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }


        [Fact(Skip = "This is for development purposes only")]
        public void OpenFile()
        {
            var fileName = @"file:///C:/dev/git/BluePrism.WordLadder/answer.txt";
            new OpenFileHelper().OpenFile(fileName);
        }
    }
}