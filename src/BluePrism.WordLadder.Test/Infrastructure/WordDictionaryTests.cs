using System.Collections.Generic;
using System.IO;
using System.Text;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class WordDictionaryTests
    {
        private readonly IWordDictionaryService _sut;
        private readonly IDictionaryPreprocessService _dictionaryPreprocessService;
        private readonly IFileWrapper _fileWrapper;

        public WordDictionaryTests()
        {
            _dictionaryPreprocessService = Substitute.For<IDictionaryPreprocessService>();
            _fileWrapper = Substitute.For<IFileWrapper>();
            _sut = new WordDictionaryService(_fileWrapper, _dictionaryPreprocessService);
        }

        [Fact]
        public void Initialise_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange 
            var fileName = "/throwerror.txt";
            var startWord = "ABCD";
            var endWord = "WXYZ";
            _fileWrapper.When(f => f.FileExists(Arg.Is<string>(fileName)))
                .Do(f => throw new FileNotFoundException());

            var wordDictionary = new WordDictionaryService(_fileWrapper, _dictionaryPreprocessService);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
                wordDictionary.Initialise(new Options(startWord, endWord, fileName, "answer.txt")));
        }

        [Fact]
        public void GetListOfWords_WhenFileExists_ReturnsListOfWords()
        {
            // Act
            var result = _sut.GetWordDictionary();

            // Assert
            result.Keys.Should().AllBeAssignableTo<string>();
            result.Values.Should().AllBeAssignableTo<bool>();
        }

        [Fact]
        public void GetPreprocessedWordsDictionary_ReturnsDictionaryOfStringAndCollectionOfStrings()
        {
            // Arrange
            _fileWrapper.FileExists(Arg.Any<string>());

            var sb = new StringBuilder();
            sb.AppendLine("A");
            sb.AppendLine("B");
            sb.AppendLine("C");
            var wordDictionaryFile = Encoding.UTF8.GetBytes(sb.ToString());
            var fakeMemoryStream = new MemoryStream(wordDictionaryFile);

            StreamReader stream = null;
            _fileWrapper.StreamReader(Arg.Any<string>())
                .Returns(stream = new WordDictionaryStreamReader(fakeMemoryStream));

            _dictionaryPreprocessService
                .When(service => service.CreatePreprocessedDictionaries(Arg.Is(
                    new DictionaryPreprocessServiceParams("A",
                        new Dictionary<string, bool>(),
                        new Dictionary<string, ICollection<string>>()))))
                .Do(d => d.ArgAt<DictionaryPreprocessServiceParams>(0).ListOfPreprocessedWords
                    .Add("*", new List<string>() {"A", "B", "C"}));

            // Act
            var result = _sut.GetPreprocessedWordsDictionary();

            // Assert
            result.Keys.Should().ContainItemsAssignableTo<string>();

            result.Values.Should().ContainItemsAssignableTo<IEnumerable<string>>();

            stream.Dispose();
        }
    }
}