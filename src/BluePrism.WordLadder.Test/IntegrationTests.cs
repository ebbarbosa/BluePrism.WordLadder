using System;
using System.Collections.Generic;
using System.IO;
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
            OpenFileHelper.OpenFile(fileName);
        }

        //todo: delete 
        [Fact]
        public void DEleteAfterSucceeded()
        {
            var result = new Solution().LadderLength("hit", "cog", new List<string>() { "hot", "dot", "dog", "lot", "log", "cog" });
            Assert.Equal(5, result);
        }

        [Fact]
        public void DEleteAfterSucceeded_startWordAndEndWord_inWordList()
        {
            var result = new Solution().LadderLength("a", "c", new List<string>() { "a", "b", "c" });
            Assert.Equal(2, result);
        }

        [Fact]
        public void DEleteAfterSucceeded_3startWordAndEndWord_inWordList()
        {
            var result = new Solution().LadderLength("aaa", "ccc", new List<string>() { "aaa", "aba", "abc", "aca", "acd", "ccd", "ccc" });

            Assert.Equal(5, result);
        }
    }
}