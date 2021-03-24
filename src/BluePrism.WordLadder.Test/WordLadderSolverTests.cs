using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using BluePrism.WordLadder.Domain.Models;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace BluePrism.WordLadder.Test
{

    public class WordLadderSolverTests
    {
        readonly IWordLadderSolver _sut;
        private readonly IGetSimilarWordsFromProcessedListService _getWordFromProcessedListService;

        public WordLadderSolverTests()
        {
            _getWordFromProcessedListService = Substitute.For<IGetSimilarWordsFromProcessedListService>();
            _sut = new WordLadderSolver(_getWordFromProcessedListService);
            _getWordFromProcessedListService.ClearReceivedCalls();
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasNoWords_ReturnsEmptyList()
        {
            // Arrange
            var beginWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>();
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();
            HashSet<string> wordsFound = new HashSet<string>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Any<string>(),
                Arg.Any<IDictionary<string, ICollection<string>>>()).Returns(wordsFound);

            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(beginWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasNoTargetWord_ReturnsEmptyList()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>()
            {
                { "HARD" , false }
            };
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();
            _getWordFromProcessedListService.GetSimiliarWords(Arg.Any<string>(), Arg.Is(wordDicPreProcessed))
                .ReturnsForAnyArgs(new HashSet<string>());

            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasOnlyFirstWord_ReturnsEmptyList()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>()
            {
                {"HARD", false}
            };
            var wordsFound = new HashSet<string>();
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is(firstWord), Arg.Is(wordDicPreProcessed))
                .ReturnsForAnyArgs(wordsFound);

            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoSeedWords_ReturnsAListWithThemBothOrdered()
        {
            // Arrange
            var firstWord = "HARE";
            var targetWord = "DARE";
            var wordDic = new Dictionary<string, bool>() {
                { "HARE", false },
                { "DARE", false }
            };
            var wordsFound = new HashSet<string>() { "HARE" };
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is(targetWord), Arg.Is(wordDicPreProcessed))
                .Returns(wordsFound);

            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_Has3Words_AndThirdWordIsNotSimiliarToOthers_ReturnsAListWithOnlyFirstAndTargetWord()
        {
            // Arrange
            var firstWord = "BARD";
            var thirdWord = "FOXY";
            var targetWord = "HARD";
            var wordDic = new Dictionary<string, bool>()
            {
                {firstWord, false},
                {thirdWord, false},
                {targetWord, false}
            };
            var wordsFound = new HashSet<string>() { "BARD" };
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is(firstWord), Arg.Is(wordDicPreProcessed))
                .ReturnsForAnyArgs(wordsFound);

            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoWordsAndALinkingWord_ReturnsAListWithThemBothAndALinkWordBetweenOrdered()
        {
            // Arrange
            var firstWord = "HARE";
            var targetWord = "DATE";
            var linkWord = "DARE";
            var wordDic = new Dictionary<string, bool>()
            {
                {"HARE", false},
                {"DARE", false},
                {"DATE", false}
            };
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is(targetWord),
                    Arg.Any<IDictionary<string, ICollection<string>>>())
                .Returns(new HashSet<string>(new[] { linkWord }));
            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is(linkWord),
                    Arg.Any<IDictionary<string, ICollection<string>>>())
                .Returns(new HashSet<string>(new[] { firstWord }));

            var expectedResult = new List<string>() {
                firstWord, linkWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoWordsAndAWordWith5Letters_ReturnsAListWithThemBothOrdered()
        {
            // Arrange
            var firstWord = "XXXZ";
            var targetWord = "XXZZ";

            var wordDic = new Dictionary<string, bool>() {
                {"DARET", false},
                {"XXXZ", false},
                {"XXZZ", false}
            };
            var wordDicPreProcessed = new Dictionary<string, ICollection<string>>();

            _getWordFromProcessedListService.GetSimiliarWords(Arg.Is("XXZZ"),
                    Arg.Any<IDictionary<string, ICollection<string>>>())
                .Returns(new HashSet<string>(new[] { "XXXZ" }));

            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic, wordDicPreProcessed);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }


    }
}