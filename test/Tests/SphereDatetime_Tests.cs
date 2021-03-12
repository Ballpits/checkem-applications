using System;
using Sphere.DateTime;
using Xunit;

namespace Library.Tests
{
    public class SphereDatetime_Tests
    {
        [Theory]
        [InlineData(12, 11)]
        [InlineData(11, 10)]
        [InlineData(5, 4)]
        [InlineData(3, 2)]
        public void Test_WillGetPreviousMonth(int month, int expected)
        {
            int actual = DateTimeCalculator.GetPreviousMonth(month);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_WillGetDecemberIfIsJenuary()
        {
            int expected = 12;

            int actual = DateTimeCalculator.GetPreviousMonth(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_WillGetPreviousMonthOfCurrentMonth()
        {
            int expected = DateTimeCalculator.GetPreviousMonth(DateTime.Now.Month);

            int actual = DateTimeCalculator.PreviousMonth;

            Assert.Equal(expected, actual);
        }
    }
}
