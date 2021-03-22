using BluePrism.WordLadder.Models.Extensions;
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

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenCallerWordIsEmpty_ReturnsFalse()
        {
            string sut = string.Empty;
            var subject = "KART";

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenCallerWordIsWhiteSpace_ReturnsFalse()
        {
            string sut = "";
            var subject = "KART";

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsNull_ReturnsFalse()
        {
            string sut = "TEST";
            string subject = null;

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsEmpty_ReturnsFalse()
        {
            string sut = "TESTING";
            var subject = string.Empty;

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Fact]
        public void IsSimilarBy_WhenTargetWordIsWhiteSpace_ReturnsFalse()
        {
            string sut = "TESTING";
            var subject = "";

            var result = sut.IsDifferentOnlyBy(subject, Constants.NumCharsToDiffer);

            Assert.False(result);
        }

        [Theory]
        [InlineData("KART", "ARTX")]
        [InlineData("KART", "RTAT")]
        [InlineData("KART", "KATX")]
        [InlineData("KART", "TKAR")]
        [InlineData("HBRD", "DARY")]
        [InlineData("HACD", "HARY")]
        [InlineData("LIDA", "OILY")]
        [InlineData("AXDA", "XABY")]
        [InlineData("AAAX", "ZAAY")]
        [InlineData("DDDZ", "ADDY")]
        [InlineData("DDDA", "BDDY")]
        [InlineData("DDXZ", "DDDY")]
        [InlineData("DDDZ", "DXDY")]
        [InlineData("HARD", "DARE")]
        [InlineData("LISO", "OILO")]
        [InlineData("AXXA", "XAAZ")]
        [InlineData("AAAX", "ZAAA")]
        [InlineData("DDDZ", "DXDD")]
        public void IsSimilarBy_WhenWordsDifferByMoreThanOneChar_ReturnsFalse(string sut, string subject)
        {
            var result = sut.IsDifferentOnlyBy(subject, 1);

            Assert.False(result);
        }

        [Theory]
        [InlineData("HARD", "HARE")]
        [InlineData("DDZD", "DDXD")]
        [InlineData("DDDZ", "DDXZ")]
        [InlineData("DDDZ", "DZDZ")]
        [InlineData("DDDZ", "ZDDZ")]
        [InlineData("ABCZ", "XBCZ")]
        [InlineData("DDDZ", "XDDZ")]
        [InlineData("XDDZ", "XADZ")]
        public void IsSimilarBy_WhenWordsDifferByOneChar_ReturnsTrue(string sut, string subject)
        {
            var result = sut.IsDifferentOnlyBy(subject, 1);

            Assert.True(result);
        }

        [Theory]
        [InlineData("AAAA", "AAAA")]
        [InlineData("AAXX", "XXAA")]
        [InlineData("XAAX", "XAAX")]
        [InlineData("AXAX", "XAXA")]
        [InlineData("ABCD", "DCBA")]
        [InlineData("XDDD", "DDDX")]
        [InlineData("DDDZ", "DZDD")]
        [InlineData("AAAB", "AABA")]
        [InlineData("AABA", "ABAA")]
        public void IsDifferentOnlyBy_GivenAnagramsMakeThePathLonger_ReturnsFalse(string sut, string subject)
        {
            var result = sut.IsDifferentOnlyBy(subject, 1);

            Assert.False(result);
        }
    }
}
