using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Collections.Generic;
using BluePrism.WordLadder.Models;

namespace BluePrism.WordLadder.Test
{

    public class WordLadderSolverTests
    {
        IWordLadderSolver _sut;

        public WordLadderSolverTests()
        {
            _sut = new WordLadderSolver();
        }

        [Fact]
        public void Solve_WhenWordDictionary_HasOnlyFirstWord_ReturnsEmptyList()
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
        public void Solve_WhenWordDictionary_HasTheTwoWords_ReturnsAListWithThemBoth_FirstWord_First()
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
        public void Solve_WhenWordDictionary_Has3Words_AndThirdWordDoesNotMatchFirstWordChars_ReturnsAListWithOnlyFirstAndTargetWord_FirstWord_First()
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
        public void Solve_WhenWordDictionary_HasTheTwoWordsAndAnotherWord_ReturnsAListWithThemBothAndALinkWordBetween_FirstWord_First()
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
    }
}