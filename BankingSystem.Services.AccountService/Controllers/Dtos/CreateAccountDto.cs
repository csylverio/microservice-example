using System;

namespace BankingSystem.Services.AccountService.Controllers.Dtos;

public class CreateAccountDto
{
    public string AccountNumber { get; set; }
    public decimal InitialBalance { get; set; }
}
