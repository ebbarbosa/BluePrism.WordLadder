using BluePrism.WordLadder.Extensions;
using System;
using Xunit;

namespace BluePrism.WordLadder.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsSimilarBy_WhenCallerWordIsNull_ReturnsFalse()
        {
            string sut = null;
            var subject = "KART";

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenCallerWordIsEmpty_ReturnsFalse()
        {
            string sut = string.Empty;
            var subject = "KART";

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenCallerWordIsWhiteSpace_ReturnsFalse()
        {
            string sut = "";
            var subject = "KART";

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsNull_ReturnsFalse()
        {
            string sut = "TEST";
            string subject = null;

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsEmpty_ReturnsFalse()
        {
            string sut = "TESTING";
            var subject = string.Empty;

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsWhiteSpace_ReturnsFalse()
        {
            string sut = "TESTING";
            var subject = "";

            var result = sut.IsSimilarBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenWordsDifferByMoreThanOneChar_ReturnsFalse()
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
        public void IsSimilarBy_WhenWordsDifferByOneChar_ReturnsTrue(String sut, string subject)
        {
            var result = sut.IsSimilarBy(subject, 1);

            Assert.True(result);
        }
    }
}
