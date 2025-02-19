public class SmplStringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
            return 0;

        // Get delimiters

        var delimiters = GetDelimiters(numbers);

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
        Validate(numberList);


        // Ignore numbers greater than 1000
        return numberList.Where(n => n <= 1000).Sum();
    }
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