using MediatR;

namespace StringCalculator.Application.Commands
{
    public class AddNumbersCommand : IRequest<int>
    {
        public string Numbers { get; set; }
    }
}