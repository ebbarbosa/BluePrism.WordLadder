using System.Collections.Generic;
using FluentAssertions;
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
        public void Write()
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
    }
}

