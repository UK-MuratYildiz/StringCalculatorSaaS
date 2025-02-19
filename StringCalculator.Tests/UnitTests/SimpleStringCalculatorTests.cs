
using System;
using Xunit;
namespace StringCalculator.Tests.UnitTests
{
    public class SimpleStringCalculatorTests
    {
        private readonly SmplStringCalculator _calculator;

        public SimpleStringCalculatorTests()
        {

            _calculator = new SmplStringCalculator();
        }

        [Fact]
        public void Add_EmptyString_ReturnsZero()
        {
            var result = _calculator.Add("");
            Assert.Equal(0, result);
        }

        [Fact]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var result = _calculator.Add("5");
            Assert.Equal(5, result);
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var result = _calculator.Add("1,2");
            Assert.Equal(3, result);
        }

        [Fact]
        public void Add_NewLineDelimiter_ReturnsSum()
        {
            var result = _calculator.Add("1\n2,3");
            Assert.Equal(6, result);
        }

        [Fact]
        public void Add_InvalidInput_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _calculator.Add("1,\n"));
            Assert.Equal("Invalid input: Trailing delimiter found.", exception.Message);
        }

        [Fact]
        public void Add_CustomDelimiter_ReturnsSum()
        {
            var result = _calculator.Add("//;\n1;2");
            Assert.Equal(3, result);
        }

        [Fact]
        public void Add_NegativeNumbers_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _calculator.Add("1,-2,3,-4"));
            Assert.Equal("Negatives not allowed: -2, -4", exception.Message);
        }

        [Fact]
        public void Add_NumbersGreaterThan1000_Ignored()
        {
            var result = _calculator.Add("2,1001,13");
            Assert.Equal(15, result);
        }

        [Fact]
        public void Add_MultipleCustomDelimiters_ReturnsSum()
        {
            var result = _calculator.Add("//*%\n1*2%3");
            Assert.Equal(6, result);
        }
    }
}
