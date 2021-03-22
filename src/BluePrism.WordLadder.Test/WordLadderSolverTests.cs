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
            var wordDic = new HashSet<string>();
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
            var wordDic = new HashSet<string>()
            {
                { "HARD" }
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
            var wordDic = new HashSet<string>(){
                {"HARD"}
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
            var firstWord = "HARE";
            var targetWord = "DARE";
            var wordDic = new HashSet<string>() {
                { "HARE" },
                { "DARE" }
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
            var firstWord = "BARD";
            var targetWord = "HARD";
            var wordDic = new HashSet<string>() {
                {"BARD" },
                {"HARD" }
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
            var firstWord = "HARE";
            var targetWord = "DATE";
            var linkWord = "DARE";
            var wordDic = new HashSet<string>() {
                {"HARE" },
                {"DARE" },
                {"DATE" }
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
            var firstWord = "XXXZ";
            var targetWord = "XXZZ";

            var wordDic = new HashSet<string>() {
                {"DARET"},
                {"XXXZ"},
                {"XXZZ"}
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

            var linkingWord = "AAAE"; // if removed it will generate a longer path, this is important to test if the BFS algo is working.

            var wordDic = new HashSet<string>() {
                {"AAAA" },
                {"ABAA" },
                {"BBAA" },
                {"BBBA" },
                {"BBAC" },
                {"ABXA" },
                {"AAXA" },
                {"ACBA" },
                {"AABA" },
                {"ABBA" },
                {"ABBB" },
                {"ABCC" },
                {"ACCA" },
                {"BAAA" },
                {"BAAE" },
                {"BADE" },
                {"BADE" },
                {"ACAE" },
                {"AAED" },
                {"ADDE" },
                {linkingWord},
                {"AADE" }
            };
            var expectedResult = new List<string>()
                {firstWord, linkingWord, targetWord};

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

            var wordDic = new HashSet<string>() {
                {"AAAA" },
                {"ABAA" },
                {"BBAA" },
                {"BBBA" },
                {"BBAC" },
                {"ABXA" },
                {"AAXA" },
                {"ACBA" },
                {"AABA" },
                {"ABBA" },
                {"ABBB" },
                {"ABCC" },
                {"ACCA" },
                {"ACAE" },
                {"AAED" },
                {"ADDE" },
                {"AAAE" },
                {"AADE" }
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