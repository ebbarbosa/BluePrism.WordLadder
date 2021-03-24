using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using CommandLine;
using FluentAssertions;
using Xunit;
using CommandLine;

namespace BluePrism.WordLadder.Test
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
            
            _sut = new CommandLineWrapper(args);

            var result = _sut.GetResult();

            //result.Should().BeEquivalentTo(expectedResult);
        }

    }
}
