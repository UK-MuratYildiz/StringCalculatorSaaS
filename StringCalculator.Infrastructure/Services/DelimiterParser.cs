using StringCalculator.Domain.Interfaces;

namespace StringCalculator.Infrastructure.Services
{
    public class DelimiterParser : IDelimiterParser
    {
        public List<string> GetDelimiters(string input)
        {
            var delimiters = new List<string> { ",", "\n" }; // Default delimiters

            if (input.StartsWith("//"))
            {
                var delimiterSectionEnd = input.IndexOf('\n');
                if (delimiterSectionEnd == -1)
                    throw new ArgumentException("Invalid input: Custom delimiter format is incorrect.");

                var delimiterSection = input.Substring(2, delimiterSectionEnd - 2); // Extract delimiter section

                // Handle multiple custom delimiters (e.g., "//*%\n1*2%3")
                if (delimiterSection.Length > 1)
                {
                    // Treat each character as a separate delimiter
                    delimiters.AddRange(delimiterSection.Select(c => c.ToString()));
                }
                else
                {
                    // Single custom delimiter
                    delimiters.Add(delimiterSection);
                }
            }

            return delimiters;
        }
    }
}


