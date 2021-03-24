using System;
using System.Collections.Generic;
using BluePrism.WordLadder.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class GetIndexOfTheWordServiceTests
    {
        private readonly GetSimilarWordsFromProcessedListService _sut;

        public GetIndexOfTheWordServiceTests()
        {
            _sut = new GetSimilarWordsFromProcessedListService();
        }

        [Fact]
        public void GetIndex_WhenWordIsNotFoundInThePreprocessedList_ReturnsMinusOne()
        {
            string word = null;
            IDictionary<string, ICollection<string>> wildCardsdict = null;

            var resultIndex = _sut.GetSimiliarWords(word, wildCardsdict);

            resultIndex.Should().BeEmpty();
        }

        [Fact]
        public void GetIndex_WhenWildcardWordIsFoundInThePreprocessedList_ReturnsAllPossibleWords()
        {
            string word = "dog";
            var wildCardsdict = new Dictionary<string, ICollection<string>>
            {
                {"*od", new List<string> {"lod"}},
                {"l*d", new List<string> {"lod"}},
                {"lo*", new List<string> {"lod", "log", "lov"}},
                {"d*g", new List<string> {"dog"}},
                {"do*", new List<string> {"dog", "dos"}},
                {"*og", new List<string> {"log", "cog", "dog"}},
                {"l*g", new List<string> {"log", "cog"}},
                {"c*g", new List<string>{"cog"}},
                {"co*", new List<string>{"cog"}},
                {"*ov", new List<string>{"lov"}},
                {"l*v", new List<string>{"lov"}},
            };

            var resultIndex = _sut.GetSimiliarWords(word, wildCardsdict);

            resultIndex.Should().BeEquivalentTo(new[] { "dog", "dos", "log", "cog" });
        }

    }
}