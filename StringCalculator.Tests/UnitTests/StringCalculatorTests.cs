using Moq;
using StringCalculator.Application.Commands;
using StringCalculator.Application.Handlers;
using StringCalculator.Domain.Interfaces;
using System;
using Xunit;

namespace StringCalculator.Tests.UnitTests
{
    public class StringCalculatorTests
    {
        private readonly Mock<IDelimiterParser> _mockDelimiterParser;
        private readonly Mock<INumberValidator> _mockNumberValidator;
        private readonly AddNumbersCommandHandler _handler;

        public StringCalculatorTests()
        {
            _mockDelimiterParser = new Mock<IDelimiterParser>();
            _mockNumberValidator = new Mock<INumberValidator>();
            _handler = new AddNumbersCommandHandler(_mockDelimiterParser.Object, _mockNumberValidator.Object);
        }

        [Fact]
        public async Task Add_EmptyString_ReturnsZero()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "" };
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Add_SingleNumber_ReturnsNumber()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "5" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { "," });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task Add_TwoNumbers_ReturnsSum()
        {
            //Arrange
            var command = new AddNumbersCommand { Numbers = "1,2" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { "," });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task Add_NewLineDelimiter_ReturnsSum()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "1\n2,3" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { ",", "\n" });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public async Task Add_InvalidInputWithTrailingDelimiter_ThrowsException()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "1,\n" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { ",", "\n" });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            // Assert
            Assert.Equal("Invalid input: Trailing delimiter found.", exception.Message);
        }

        [Fact]
        public async Task Add_CustomDelimiter_ReturnsSum()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "//;\n1;2" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { ";" });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task Add_NegativeNumbers_ThrowsException()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "1,-2,3,-4" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { "," });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>())).Throws(new ArgumentException("Negatives not allowed: -2, -4"));
            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            // Assert
            Assert.Equal("Negatives not allowed: -2, -4", exception.Message);
        }

        [Fact]
        public async Task Add_NumbersGreaterThan1000_Ignored()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "2,1001,13" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { "," });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(15, result);
        }

        [Fact]
        public async Task Add_MultipleCustomDelimiters_ReturnsSum()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "//*%\n1*2%3" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { "*", "%" });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public async Task Add_CaseInsensitiveNonAlphabeticalDelimiters_ReturnsSum()
        {
            // Arrange
            var command = new AddNumbersCommand { Numbers = "//;\n1;2;3" };
            _mockDelimiterParser.Setup(p => p.GetDelimiters(It.IsAny<string>())).Returns(new List<string> { ";" });
            _mockNumberValidator.Setup(v => v.Validate(It.IsAny<List<int>>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(6, result);
        }
    }
}