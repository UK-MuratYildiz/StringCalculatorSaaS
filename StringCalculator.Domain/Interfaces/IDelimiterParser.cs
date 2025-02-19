namespace StringCalculator.Domain.Interfaces
{
    public interface IDelimiterParser
    {
        List<string> GetDelimiters(string input);
    }
}
