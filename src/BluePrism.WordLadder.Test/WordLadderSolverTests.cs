using BluePrism.WordLadder.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BluePrism.WordLadder.Test
{

    public class WordLadderSolverTests
    {
        readonly IWordLadderSolver _sut;

        public WordLadderSolverTests()
        {
            _sut = new WordLadderSolver();
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasNoWords_ReturnsEmptyList()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>();
            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasNoTargetWord_ReturnsEmptyList()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>(){
                {"HARD", false }
            };
            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasOnlyFirstWord_ReturnsEmptyList()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "BOIL";
            var wordDic = new Dictionary<string, bool>(){
                {"HARD", false }
            };
            var expectedResult = Enumerable.Empty<string>();

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoSeedWords_ReturnsAListWithThemBothOrdered()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "DARE";
            var wordDic = new Dictionary<string, bool>(){
                {"DARE", false },
                {"HARD", false }
            };
            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_Has3Words_AndThirdWordDoesNotMatchFirstWordChars_ReturnsAListWithOnlyFirstAndTargetWord_FirstWord_First()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "DARE";
            var wordDic = new Dictionary<string, bool>(){
                {"DARE", false },
                {"HARD", false }
            };
            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoWordsAndALinkingWord_ReturnsAListWithThemBothAndALinkWordBetweenOrdered()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "DATE";
            var linkWord = "DARE";
            var wordDic = new Dictionary<string, bool>(){
                {"DARE", false },
                {"HARD", false },
                {"DATE", false }
            };
            var expectedResult = new List<string>() {
                firstWord, linkWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoWordsAndAWordWith5Letters_ReturnsAListWithThemBothOrdered()
        {
            // Arrange
            var firstWord = "HARD";
            var targetWord = "DART";

            var wordDic = new Dictionary<string, bool>(){
                {"DARET", false },
                {"HARD", false },
                {"DART", false }
            };

            var expectedResult = new List<string>() {
                firstWord, targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasOnlyOnePathToTarget_ReturnsThePath()
        {
            // Arrange
            var firstWord = "AAAA";
            var targetWord = "AADE";

            var wordDic = new Dictionary<string, bool>(){
                {"AAAA", false },
                {"ABAA", false },
                {"BBAA", false },
                {"BBBA", false },
                {"BBAC", false },
                {"ABXA", false },
                {"AAXA", false },
                {"ACBA", false },
                {"AABA", false },
                {"ABBA", false },
                {"ABBB", false },
                {"ABCC", false },
                {"ACCA", false },
                {"ACAE", false },
                {"AAED", false },
                {"ADDE", false },
                //{"AAAE", false }, -- this will be used in the next test to prove the shortest path is returned.
                {"AADE", false }
            };
            var expectedResult = new List<string>()
                {firstWord, "ABAA", "ACBA", "ACAE", targetWord};

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }

        [Fact]
        public void SolveLadder_WhenWordDictionary_HasTheTwoPathsToTarget_ReturnsTheShortestPath()
        {
            // Arrange
            var firstWord = "AAAA";
            var targetWord = "AADE";

            var wordDic = new Dictionary<string, bool>(){
                {"AAAA", false },
                {"ABAA", false },
                {"BBAA", false },
                {"BBBA", false },
                {"BBAC", false },
                {"ABXA", false },
                {"AAXA", false },
                {"ACBA", false },
                {"AABA", false },
                {"ABBA", false },
                {"ABBB", false },
                {"ABCC", false },
                {"ACCA", false },
                {"ACAE", false },
                {"AAED", false },
                {"ADDE", false },
                {"AAAE", false },
                {"AADE", false }
            };
            var expectedResult = new List<string>() {
                firstWord, "AAAE", targetWord
            };

            // Act
            var result = _sut.SolveLadder(firstWord, targetWord, wordDic);

            // Assert
            result.Should().NotBeEmpty().And.ContainInOrder(expectedResult);
        }
    }
}