using BluePrism.WordLadder.Infrastructure;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace BluePrism.WordLadder.Test
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


    public class FileWrapperTester : FileWrapper
    {
        public IList<string> WordLadder { get; private set; }

        public string FileName { get; private set; }

        protected override void WriteAllLines(IList<string> wordLadder, string fileName)
        {
            WordLadder = wordLadder;
            FileName = fileName;
        }
    }
}

