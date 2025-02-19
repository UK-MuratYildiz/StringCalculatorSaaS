using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace SimpleStringCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleStringCalculatorController : ControllerBase
    {

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] string numbers)

        {
            SmplStringCalculator _smplStringCalculator = new SmplStringCalculator();
            try
            {
                var result = _smplStringCalculator.Add(numbers );
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
