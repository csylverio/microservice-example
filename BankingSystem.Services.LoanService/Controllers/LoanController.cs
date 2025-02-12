using BankingSystem.Services.LoanService.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankingSystem.Services.LoanService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private const string accountServiceUrl = "http://account-service:5000";

    [HttpPost("check-loan-available")]
    public async Task<IActionResult> CheckLoanAvailable([FromBody] CheckLoanAvailableDto checkLoanAvailableDto)
    {
        // realiza chamada HTTP para API Account Service
        using var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync($"{accountServiceUrl}/api/Account/{checkLoanAvailableDto.AccountNumber}");
        string responseData = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response from Account Service: {responseData}");
        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Account not found");
        }

        // Desserializa o JSON em um objeto Account
        Account account = JsonSerializer.Deserialize<Account>(responseData);

        // Permite 50% de emprestimo do saldo da conta corrente
        var availableValue = account.Balance * 0.5m;

        // Criando um objeto anônimo para retorno
        var loanAvailable = new
        {
            approved = availableValue >= checkLoanAvailableDto.Amount,
            amountAvailable = availableValue,
            amountRequested = checkLoanAvailableDto.Amount
        };
        return Ok(loanAvailable);
    }
}
