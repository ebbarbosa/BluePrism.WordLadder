using BluePrism.WordLadder.Infrastructure;
using FluentAssertions;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class FactoryTests
    {

        [Fact]
        public void CreateWordLadderSolver_ReturnsWordLadderClassImplementationOfIWordLadder()
        {
            var result = Factory.CreateWordLadderSolver();

            result.Should().BeOfType<WordLadderSolver>().And.BeAssignableTo<IWordLadderSolver>();
        }

        [Fact]
        public void CreateFileWrapper_ReturnsFileWrapperClassImplementationOfIFileWrapper()
        {
            var result = Factory.CreateFileWrapper();

            result.Should().BeOfType<FileWrapper>().And.BeAssignableTo<IFileWrapper>();
        }

        [Fact]
        public void CreateWordDictionaryService_ReturnsWordDictionaryServiceClassImplementationOfIWordDictionaryService()
        {
            var result = Factory.CreateWordDictionaryService();

            result.Should().BeOfType<WordDictionaryService>().And.BeAssignableTo<IWordDictionaryService>();
        }

        [Fact]
        public void CreateCommandLineWrapper_ReturnsCommandLineParserClassImplementationOfIWordDictionary()
        {
            var result = Factory.CreateCommandLineWrapper();

            result.Should().BeOfType<CommandLineWrapper>().And.BeAssignableTo<ICommandLineWrapper>();
        }
    }
}