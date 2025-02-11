using BankingService.Services.LoanService.Controllers.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingService.Services.LoanService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
     private const string accountServiceUrl = "https://localhost:5002";

    [HttpPost]
    public async Task<IActionResult> CheckAvailableLoan([FromBody] RequestLoanDto dto)
    {
        //1. obtem informações de transações financeiras da conta do cliente
        
        // realiza chamada HTTP para API Account Service
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{accountServiceUrl}/api/Account/{dto.AccountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Erro ao obter extrato do cliente. StatusCode:{response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }

        //verifica se o cliente tem saldo suficiente para o empréstimo
        //verifica se o cliente já não tem um empréstimo ativo
        //verifica se o cliente já não tem uma solicitação de empréstimo ativa
        //analisa valor disponível para empréstimo
        //retorna o empréstimo dispnível
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ProvideLoan([FromBody] RequestLoanDto dto)
    {
        // cria emprestimo para cliente e retorna o emprestimo criado
        return Ok();
    }
}
