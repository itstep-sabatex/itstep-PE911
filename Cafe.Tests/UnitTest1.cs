using Cafe.Data;
using System;
using Xunit;

namespace Cafe.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(10,2,5)]
        [InlineData(10,0,-1)]
        public void DivPositive(double a,double b,double result)
        {
            Assert.Equal(result, DemoTest.DivPositive(a, b));
        }
    }
}
