using System;

namespace BankingSystem.Services.LoanService.Controllers.Dtos;

public class Account
{
    public string Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public Transaction[] Transactions { get; set; }
}
