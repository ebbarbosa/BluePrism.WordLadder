using System.Collections.Generic;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace BluePrism.WordLadder.Test.Infrastructure
{
    public class DictionaryPreprocessServiceTests
    {
        readonly IDictionaryPreprocessService _sut;

        public DictionaryPreprocessServiceTests()
        {
            _sut = new DictionaryPreprocessService();
        }

        [Fact]
        public void CreatePreprocessedDictionaries_SetsWordIntoListOfWords_AndTheWordListOfPreprocessedWords()
        {
            IDictionary<string, ICollection<string>> listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();
            IDictionary<string, bool> listOfWords = new Dictionary<string, bool>();
            string line = "a";
            var expectedPreprocessedValues = new KeyValuePair<string, ICollection<string>>("*", new[] { line });

            _sut.CreatePreprocessedDictionaries(new DictionaryPreprocessServiceParams(line, listOfWords, listOfPreprocessedWords));

            listOfWords.Keys.Should().Contain(line);
            listOfWords.Values.Should().Contain(false);

            listOfPreprocessedWords.Keys.Should().Contain("*");
            listOfPreprocessedWords.Values.Should().AllBeEquivalentTo(expectedPreprocessedValues.Value);
        }

        [Theory]
        [InlineData("*", "a","b","c")]
        public void CreatePreprocessedDictionaries_WhenCalledMultipleTimesWithCorrelatedWords_SetsSameWildcardAsKey_AndAllTheWordsToThatKeysPreprocessedWords
            (string key,  params string[] expectedPreprocessedValues)
        {
            IDictionary<string, ICollection<string>> listOfPreprocessedWords = new Dictionary<string, ICollection<string>>();
            IDictionary<string, bool> listOfWords = new Dictionary<string, bool>();

            foreach (var line in expectedPreprocessedValues)
            {
                _sut.CreatePreprocessedDictionaries(new DictionaryPreprocessServiceParams(line, listOfWords, listOfPreprocessedWords));    
            }
            
            listOfWords.Keys.Should().Contain(expectedPreprocessedValues);
            listOfWords.Values.Should().Contain(false);

            listOfPreprocessedWords.Keys.Should().Contain(key);
            listOfPreprocessedWords.Values.Should().AllBeEquivalentTo(expectedPreprocessedValues);
        }
    }
}