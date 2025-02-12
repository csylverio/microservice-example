using System;

namespace BankingSystem.Services.LoanService.Controllers.Dtos;

public class Transaction
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public DateTime Date { get; set; }
}
