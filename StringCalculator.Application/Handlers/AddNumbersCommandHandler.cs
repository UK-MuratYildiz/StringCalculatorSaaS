using MediatR;
using StringCalculator.Application.Commands;
using StringCalculator.Domain.Interfaces;

namespace StringCalculator.Application.Handlers
{
    public class AddNumbersCommandHandler : IRequestHandler<AddNumbersCommand, int>
    {
        private readonly IDelimiterParser _delimiterParser;
        private readonly INumberValidator _numberValidator;

        public AddNumbersCommandHandler(IDelimiterParser delimiterParser, INumberValidator numberValidator)
        {
            _delimiterParser = delimiterParser;
            _numberValidator = numberValidator;
        }

        public Task<int> Handle(AddNumbersCommand request, CancellationToken cancellationToken)
        {
            var numbers = request.Numbers;

            if (string.IsNullOrEmpty(numbers))
                return Task.FromResult(0);

            // Get delimiters

            var delimiters = _delimiterParser.GetDelimiters(numbers);

            // Remove delimiter section from input
            if (numbers.StartsWith("//"))
            {
                var delimiterSectionEnd = numbers.IndexOf('\n');
                numbers = numbers.Substring(delimiterSectionEnd + 1);
            }

            // Validate for invalid input (e.g., "1,\n")
            if (numbers.EndsWith(",") || numbers.EndsWith("\n"))
                throw new ArgumentException("Invalid input: Trailing delimiter found.");

            // Split numbers by delimiters
            var numberList = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            // Validate numbers
            _numberValidator.Validate(numberList);

            // Ignore numbers greater than 1000
            return Task.FromResult(numberList.Where(n => n <= 1000).Sum());
        }
    }
}

