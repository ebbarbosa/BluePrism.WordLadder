using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class FileWrapperTests
    {
        private readonly FileWrapperTester _fileWrapper;

        public FileWrapperTests()
        {

            this._fileWrapper = new FileWrapperTester();
        }

        [Fact]
        public void Write_ShouldSetPublicProperties()
        {
            // Arrange
            IList<string> wordLadder = new List<string>() { };
            string fileName = "someFileName";

            // Act 
            _fileWrapper.Write(wordLadder, fileName);

            // Assert
            _fileWrapper.FileName.Should().Be(fileName);
            _fileWrapper.WordLadder.Should().BeEquivalentTo(wordLadder);
        }

        [Fact]
        public void Validate_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange
            string fileName = "someFileName";
            string message = "Word Dictionary file not found";

            // Act 
            var result = Assert.Throws<FileNotFoundException>(() => _fileWrapper.ValidateFile(fileName));

            // Assert
            result.Message.Should().Be(message);
        }

        [Fact]
        public void StreamReader()
        {
            // Arrange
            string fileName = $"{Directory.GetCurrentDirectory()}\\content\\words-english.txt";

            // Act 
            var streamReader = _fileWrapper.StreamReader(fileName);

            // Assert
            streamReader.Should().NotBeNull();

            streamReader.Dispose();
        }
    }
}

