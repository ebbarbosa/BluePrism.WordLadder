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
            _fileWrapper = new FileWrapperTester();
        }

        [Fact]
        public void Write_ShouldSetPublicProperties()
        {
            // Arrange
            IList<string> wordLadder = new List<string>() { };
            var fileName = "someFileName";

            // Act 
            _fileWrapper.Write(wordLadder, fileName);

            // Assert
            _fileWrapper.FileName.Should().Be(fileName);
            _fileWrapper.WordLadder.Should().BeEquivalentTo(wordLadder);
        }

        [Fact]
        public void FileExists_WhenFileExists_ReturnsTrue()
        {
            // Arrange
            var fileName = "./content/words-english.txt";

            // Act 
            var result = _fileWrapper.FileExists(fileName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WhenFileDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var fileName = "someFileName";

            // Act 
            var result = _fileWrapper.FileExists(fileName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidPath_WhenPathIsValid_ReturnsTrue()
        {
            // Arrange
            var fileName = "./word-ladder.txt";

            // Act 
            var result = _fileWrapper.IsValidPath(fileName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidPath_WhenPathIsInvalid_ReturnsFalse()
        {
            // Arrange
            var fileName = "/var/><!!!<<>>someFileName.txt";

            // Act 
            var result = _fileWrapper.IsValidPath(fileName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void StreamReader()
        {
            // Arrange
            var fileName = $"{Directory.GetCurrentDirectory()}/content/words-english.txt";

            // Act 
            var streamReader = _fileWrapper.StreamReader(fileName);

            // Assert
            streamReader.Should().NotBeNull();

            streamReader.Dispose();
        }
    }
}