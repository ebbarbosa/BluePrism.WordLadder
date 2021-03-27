using BluePrism.WordLadder.Domain.Extensions;
using BluePrism.WordLadder.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BluePrism.WordLadder.Test.Domain
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("a", new[] { "*" })]
        [InlineData("AAAA", new[] { "*AAA", "A*AA", "AA*A", "AAA*" })]
        [InlineData("ABCD", new[] { "*BCD", "A*CD", "AB*D", "ABC*" })]
        [InlineData("AXXA", new[] { "*XXA", "A*XA", "AX*A", "AXX*" })]
        public void GetWildcardWords_ReturnsAllPossibleTrasnformationWildcardedWords(string sut, string[] expectedResult)
        {
            var result = sut.GetWildcardWords();

            result.Should().BeEquivalentTo(expectedResult);
        }

    }
}
