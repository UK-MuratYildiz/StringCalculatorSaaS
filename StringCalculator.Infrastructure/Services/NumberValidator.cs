using StringCalculator.Domain.Interfaces;

namespace StringCalculator.Infrastructure.Services
{
    public class NumberValidator : INumberValidator
    {
        public void Validate(List<int> numbers)
        {
            // Validate for negative numbers
            var negatives = numbers.Where(n => n < 0).ToList();
            if (negatives.Any())
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}");

            // Validate for invalid input (e.g., "1,\n")
            if (numbers.Count == 0)
                throw new ArgumentException("Invalid input: No numbers found.");
        }
    }
}