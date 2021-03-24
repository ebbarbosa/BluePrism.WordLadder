using System.Threading.Tasks;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using FluentAssertions;
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
            string beginWord = "BEGIN";
            string targetWord = "TESTE";
            string inputFileName = "c:\\input"; ;
            string outputFileName = "c:\\output";

            var args = new[] { beginWord, targetWord, inputFileName, outputFileName };

            var expectedResult = new Options(beginWord, targetWord, inputFileName, outputFileName);

            _sut = new CommandLineWrapper(args);

            var result = _sut.GetResult();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetResults_When_OneOfTheOutputArgsIsNotPassed_ReturnsNull_AndWritesHelpTextToConsole()
        {
            _sut = new CommandLineWrapper(new[] { "" });

            var result = _sut.GetResult();

            Assert.Null(result);
        }

        [Theory]
        [InlineData("SORT", "c:\\word-dict.txt", "C:\\teste.txt")]
        [InlineData("HIRE", "c:\\word-dict.txt", "C:\\teste.txt")]
        [InlineData("HIRE", "SORT", "C:\\teste.txt")]
        [InlineData("HIRE", "SORT", "c:\\word-dict.txt")]
        public void GetResults_When_OutputArgsAreNotPassed_ReturnsNull_AndWritesHelpTextToConsole(params string[] args)
        {
            _sut = new CommandLineWrapper(args);

            var result = _sut.GetResult();

            Assert.Null(result);
        }
    }
}
