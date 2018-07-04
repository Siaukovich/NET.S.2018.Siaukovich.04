﻿namespace IEEE.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Required naming convention is not needed in the tests")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Required XML documentation is not needed in the tests")]
    public class IEEETests
    {
        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        public string IEEE_ValidInput_ValidResult(double d) => d.DoubleToStringBits();


        [Test]
        public void IEEE_Nan_ValidResult()
        {
            string result = double.NaN.DoubleToStringBits();
            string exponent = result.Substring(1, 11);
            string mantiss = result.Substring(12);

            bool onlyOneAndZeroInResult = result.All(c => c == '0' || c == '1');
            bool allOneInExponent = exponent.All(c => c == '1');
            bool notAllZeroInMantiss = mantiss.Any(c => c != '0');

            if (!onlyOneAndZeroInResult || !allOneInExponent || !notAllZeroInMantiss)
            {
                Assert.Fail();
            }
        }
    }
}
