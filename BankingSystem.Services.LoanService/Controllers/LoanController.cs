using BankingSystem.Services.LoanService.Controllers.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Services.LoanService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        [HttpPost("check-loan-available")]
        public async Task<IActionResult> CheckLoanAvailable([FromBody] CheckLoanAvailableDto checkLoanAvailableDto)
        {
            return Ok("Teste");
        }
    }
}
