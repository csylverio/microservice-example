using BankingSystem.Services.AccountService.Controllers.Dtos;
using BankingSystem.Services.AccountService.Domain;
using BankingSystem.Services.AccountService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Services.AccountService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetAccount(string accountNumber)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null) return NotFound("Conta não encontrada.");
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
    {
        var account = new Account(dto.AccountNumber, dto.InitialBalance);
        await _accountRepository.AddAsync(account);
        return CreatedAtAction(nameof(GetAccount), new { accountNumber = account.AccountNumber }, account);
    }

    [HttpPost("{accountNumber}/deposit")]
    public async Task<IActionResult> Deposit(string accountNumber, [FromBody] decimal amount)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null) return NotFound("Conta não encontrada.");

        try
        {
            account.Deposit(amount);
            await _accountRepository.UpdateBalanceAsync(account);

            return Ok(new { Message = "Depósito realizado com sucesso.", NewBalance = account.Balance });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{accountNumber}/withdraw")]
    public async Task<IActionResult> Withdraw(string accountNumber, [FromBody] decimal amount)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null) return NotFound("Conta não encontrada.");

        try
        {
            account.Withdraw(amount);
            await _accountRepository.UpdateBalanceAsync(account);
            return Ok(new { Message = "Saque realizado com sucesso.", NewBalance = account.Balance });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
    {
        try
        {
            var fromAccount = await _accountRepository.GetByAccountNumberAsync(transferDto.FromAccountNumber);
            var toAccount = await _accountRepository.GetByAccountNumberAsync(transferDto.ToAccountNumber);

            if (fromAccount == null || toAccount == null)
                throw new InvalidOperationException("Uma ou ambas as contas não foram encontradas.");

            if (fromAccount.Balance < transferDto.Amount)
                throw new InvalidOperationException("Saldo insuficiente.");

            fromAccount.Withdraw(transferDto.Amount);
            toAccount.Deposit(transferDto.Amount);

            await _accountRepository.TransferAsync(fromAccount, toAccount);
            return Ok(new
            {
                Message = "Transferência realizada com sucesso.",
                FromAccount = fromAccount.AccountNumber,
                FromAccountBalance = fromAccount.Balance,
                ToAccount = toAccount.AccountNumber,
                ToAccountBalance = toAccount.Balance
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{accountNumber}/bank_statement")]
    public async Task<IActionResult> GetBankStatement(string accountNumber)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null) return NotFound("Conta não encontrada.");

        var transactions = account.Transactions
            .OrderByDescending(t => t.Date)
            .Select(t => new
            {
                t.Amount,
                t.Date,
                t.Description
            });

        return Ok(transactions);
    }
}