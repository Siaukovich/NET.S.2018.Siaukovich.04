namespace BinaryConverter.Tests
{
    using NUnit.Framework;
    using IEEE;
    
    [TestFixture]
    public class BinaryConverterTests
    {
        [TestCase(0, 1, ExpectedResult = "0")]
        [TestCase(1, 1, ExpectedResult = "1")]
        [TestCase(4, 5, ExpectedResult = "00100")]
        [TestCase(6, 8, ExpectedResult = "00000110")]
        [TestCase(152, 10, ExpectedResult = "0010011000")]
        [TestCase(348743, 20, ExpectedResult = "01010101001001000111")]
        public string BinaryConverter_Ints_ValidResult(int value, int length) =>
            BinaryConverter.ToBinaryString(value, length);
        
        [TestCase(3.125, 5, ExpectedResult = "11001")]
        [TestCase(1.125, 4, ExpectedResult = "1001")]
        [TestCase(10.11, 24, ExpectedResult = "101000011100001010001111")]
        [TestCase(5.5, 4, ExpectedResult = "1011")]
        [TestCase(23487.2374678234, 35, ExpectedResult = "10110111011111100111100110010101011")]
        public string BinaryConverter_Floats_ValidResult(double value, int length) =>
            BinaryConverter.ToBinaryString(value, length);
    }
}