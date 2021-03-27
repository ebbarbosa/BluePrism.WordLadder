using System;
using System.IO;
using System.Linq;
using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class CommandLineWrapperTests
    {
        private CommandLineWrapper _sut;
        
        [Fact]
        public void GetResults_When_CorrectArgsArePassed_ReturnsCorrectOptions()
        {
            // Arrange 
            string beginWord = "SORT";
            string targetWord = "HIRE";
            string inputFileName = "c:\\input"; ;
            string outputFileName = "c:\\output";

            var args = new[] { beginWord, targetWord, inputFileName, outputFileName };

            var expectedResult = new Options(beginWord, targetWord, inputFileName, outputFileName);

            _sut = new CommandLineWrapper();

            var result = _sut.GetResult(args);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("SORT", "c:\\word-dict.txt", "C:\\teste.txt")]
        [InlineData("HIRE", "c:\\word-dict.txt", "C:\\teste.txt")]
        [InlineData("HIRE", "SORT", "C:\\teste.txt")]
        [InlineData("HIRE", "SORT", "c:\\word-dict.txt")]
        public void GetResults_When_OutputArgsAreNotPassed_ReturnsNull_AndWritesHelpTextToConsole(params string[] args)
        {
            _sut = new CommandLineWrapper();

            var result = _sut.GetResult(args);

            Assert.Null(result);
        }
    }
}
