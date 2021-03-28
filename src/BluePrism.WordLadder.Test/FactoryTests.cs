using System.Reflection;
using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;
using FluentAssertions;
using Ninject;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class FactoryTests
    {
        private readonly StandardKernel _kernel;

        public FactoryTests()
        {
            _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetAssembly(typeof(Factory)));
        }

        [Fact]
        public void CreateWordLadderSolver_ReturnsWordLadderClassImplementationOfIWordLadder()
        {
            var result = _kernel.Get<IWordLadderSolver>();

            result.Should().BeOfType<WordLadderSolver>().And.BeAssignableTo<IWordLadderSolver>();
        }

        [Fact]
        public void CreateFileWrapper_ReturnsFileWrapperClassImplementationOfIFileWrapper()
        {
            var result = _kernel.Get<IFileWrapper>();

            result.Should().BeOfType<FileWrapper>().And.BeAssignableTo<IFileWrapper>();
        }

        [Fact]
        public void
            CreateWordDictionaryService_ReturnsWordDictionaryServiceClassImplementationOfIWordDictionaryService()
        {
            var result = _kernel.Get<IWordDictionaryService>();

            result.Should().BeOfType<WordDictionaryService>().And.BeAssignableTo<IWordDictionaryService>();
        }

        [Fact]
        public void CreateCommandLineWrapper_ReturnsCommandLineParserClassImplementationOfIWordDictionary()
        {
            var result = _kernel.Get<ICommandLineWrapper>();

            result.Should().BeOfType<CommandLineWrapper>().And.BeAssignableTo<ICommandLineWrapper>();
        }

        [Fact]
        public void CreateWordLadderApp_ReturnsWordLadderAppClassImplementationOfIWordLadderApp()
        {
            var result = _kernel.Get<IWordLadderApp>();

            result.Should().BeOfType<WordLadderApp>().And.BeAssignableTo<IWordLadderApp>();
        }
    }
}