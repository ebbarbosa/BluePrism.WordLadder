using System;
using Xunit;
using System.Linq;
using BluePrism.WordLadder.Extensions;

namespace BluePrism.WordLadder.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void MatchSomeChars_WhenWordsDifferByMoreThanOneChar_ReturnsFalse()
        {
            var sut = "HARD";
            var subject = "KART";

            var result = sut.IsSimilarBy(subject, 1);

            Assert.False(result);
        }

        [Theory]
        [InlineData("HARD", "DARE")]
        [InlineData("HARD", "HARE")]
        [InlineData("LISO", "OILO")]
        public void MatchSomeChars_WhenWordsDifferByOneChar_ReturnsTrue(String sut, string subject)
        {
            var result = sut.IsSimilarBy(subject, 1);

            Assert.True(result);
        }
    }
}
