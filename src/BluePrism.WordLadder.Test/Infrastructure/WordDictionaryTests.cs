using System.Collections.Generic;
using System.IO;
using System.Text;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{

    public class WordDictionaryTests
    {
        private IWordDictionaryService _sut;
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
            var sourceWord = "W";
            string? message = $"";
            _fileWrapper.When(f => f.ValidateFile(Arg.Is<string>(fileName)))
                .Do(f => throw new FileNotFoundException(message));

            var wordDictionary = new WordDictionaryService(_fileWrapper, _dictionaryPreprocessService);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => wordDictionary.Initialise(fileName, sourceWord));
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
            _fileWrapper.ValidateFile(Arg.Any<string>());

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("A");
            sb.AppendLine("B");
            sb.AppendLine("C");
            byte[] wordDictionaryFile = Encoding.UTF8.GetBytes(sb.ToString());
            var fakeMemoryStream = new MemoryStream(wordDictionaryFile);

            _fileWrapper
                .When(f => f.StreamReader(Arg.Any<string>()))
                .Do((fs) => new StreamReader(fakeMemoryStream));

            _dictionaryPreprocessService
                .When(service => service.CreatePreprocessedDictionaries(Arg.Is(
                                                new DictionaryPreprocessServiceParams("A",  
                                        new Dictionary<string, bool>(), 
                                new Dictionary<string, ICollection<string>>()))))
                .Do(d => d.ArgAt<DictionaryPreprocessServiceParams>(0).ListOfPreprocessedWords.Add("*", new List<string>() { "A", "B", "C" }));
            
            // Act
            var result = _sut.GetPreprocessedWordsDictionary();

            // Assert
            result.Keys.Should().ContainItemsAssignableTo<string>();

            result.Values.Should().ContainItemsAssignableTo<IEnumerable<string>>();
        }

    }
}