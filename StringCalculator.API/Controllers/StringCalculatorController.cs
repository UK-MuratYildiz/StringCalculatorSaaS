using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringCalculator.Application.Commands;

namespace StringCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StringCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] string numbers)
        {
            try
            {
                var result = await _mediator.Send(new AddNumbersCommand { Numbers = numbers });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}