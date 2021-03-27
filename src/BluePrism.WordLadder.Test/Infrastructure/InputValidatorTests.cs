using System;
using System.IO;
using System.Net.Http.Headers;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Validators;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class InputValidatorTests
    {
        private readonly InputValidator _sut;
        private readonly string _validWordDictionaryFilePath = @"./content/words-english.txt";
        private readonly IFileWrapper _fileWrapper;
        private readonly string _validWordLadderFilePath = @"answer.txt";

        public InputValidatorTests()
        {
            _fileWrapper = Substitute.For<IFileWrapper>();
            _fileWrapper.ClearReceivedCalls();
            _fileWrapper.FileExists(Arg.Is(_validWordDictionaryFilePath)).Returns(true);
            _fileWrapper.IsValidPath(Arg.Is(_validWordLadderFilePath)).Returns(true);
            _fileWrapper.HasTxtExtension(Arg.Is(_validWordLadderFilePath)).Returns(true);

            _sut = new InputValidator(_fileWrapper);
        }

        [Fact]
        public void Validate_WhenArgsAreValid_ExecutesProgram()
        {
            // Arrange 
            var startWord = "ABCD";
            var endWord = "WXYZ";
            bool executing = false;

            Options args = new Options(startWord, endWord, _validWordDictionaryFilePath, _validWordLadderFilePath);
            
            _sut.Validate(args, (args) =>
            {
                executing = true;
            });

            Assert.True(executing);
        }

        [Fact]
        public void Validate_WhenStartWordHasWrongLength_CallsHandlesErrorsAction()
        {
            // Arrange 
            var startWord = "AABCD";
            var endWord = "WXYZ";
            bool executing = false;
            Options args = new Options(startWord, endWord, _validWordDictionaryFilePath, _validWordLadderFilePath);

            var expectedError = "'Start Word' must be 4 characters in length. You entered 5 characters.";
            string actualError = null;

            // act
            _sut.Validate(args, (options) =>
            {
                executing = true;
            }).HandleErrors(str =>
            {
                actualError = str;
            });

            // assert
            Assert.False(executing);
            actualError.Should().BeEquivalentTo(expectedError);
        }

        [Fact]
        public void Validate_WhenEndWordHasWrongLength_CallsHandlesErrorsAction()
        {
            // Arrange 
            var startWord = "ABCD";
            var endWord = "WXYZA";
            bool executing = false;
            Options args = new Options(startWord, endWord, _validWordDictionaryFilePath, _validWordLadderFilePath);

            var expectedError = "'End Word' must be 4 characters in length. You entered 5 characters.";
            string actualError = null;

            
            // act
            _sut.Validate(args, (args) =>
            {
                executing = true;
            }).HandleErrors((str) =>
            {
                actualError = str;
            });

            // assert
            Assert.False(executing);
            actualError.Should().BeEquivalentTo(expectedError);
        }

        [Fact]
        public void Validate_WhenWordDictionaryFilePathDoesNotExists_CallsHandlesErrorsAction()
        {
            // Arrange 
            var wordDictionaryFilePath = @"%TEMP%/teste.txt";
            var startWord = "ABCD";
            var endWord = "WXYZA";
            bool executing = false;

            Options args = new Options(startWord, endWord, wordDictionaryFilePath, _validWordLadderFilePath);

            var expectedError = $"Unable to find the specified file {wordDictionaryFilePath}. Please provide an existing word dictionary file.";
            string actualError = null;

            _fileWrapper.FileExists(Arg.Is(wordDictionaryFilePath)).Returns(false);

            // act
            _sut.Validate(args, (args) =>
            {
                executing = true;
            }).HandleErrors((str) =>
            {
                actualError = str;
            });

            // assert
            Assert.False(executing);
            actualError.Should().BeEquivalentTo(expectedError);
        }

        [Fact]
        public void Validate_WhenWordLadderResultFileIsAnInvalidPath_CallsHandlesErrorsAction()
        {
            // Arrange 
            var startWord = "ABCD";
            var endWord = "WXYA";
            bool executing = false;
            string wordLadderResultFilePath = @"<>testtxt.txt";

            Options args = new Options(startWord, endWord, _validWordDictionaryFilePath, wordLadderResultFilePath);

            var expectedError = $"The provided file path {wordLadderResultFilePath} is not valid. Please provide a valid file path for the answer file.";
            string actualError = null;

            // act
            _sut.Validate(args, (options) =>
            {
                executing = true;
            }).HandleErrors((str) =>
            {
                actualError = str;
            });

            // assert
            Assert.False(executing);
            actualError.Should().BeEquivalentTo(expectedError);
        }

        [Fact]
        public void Validate_WhenWordLadderResultFileHasNoTxtExtension_CallsHandlesErrorsAction()
        {
            // Arrange 
            var startWord = "ABCD";
            var endWord = "WXYA";
            bool executing = false;
            string wordLadderResultFilePath = "teste.bin";

            Options args = new Options(startWord, endWord, _validWordDictionaryFilePath, wordLadderResultFilePath);

            var expectedError =
                $"The provided file path {wordLadderResultFilePath} is not valid. Please provide a valid file path with a .txt extension for the answer file.";
            string actualError = null;

            _fileWrapper.ClearReceivedCalls();
            _fileWrapper.FileExists(Arg.Is(_validWordDictionaryFilePath)).Returns(true);
            _fileWrapper.IsValidPath(Arg.Is(wordLadderResultFilePath)).Returns(true);
            _fileWrapper.HasTxtExtension(Arg.Is(wordLadderResultFilePath)).Returns(false);

            // act
            _sut.Validate(args, (options) =>
            {
                executing = true;
            }).HandleErrors((str) =>
            {
                actualError = str;
            });

            // assert
            Assert.False(executing);
            actualError.Should().BeEquivalentTo(expectedError);
        }
    }
}