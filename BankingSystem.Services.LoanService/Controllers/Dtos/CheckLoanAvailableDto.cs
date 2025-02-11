using System;

namespace BankingSystem.Services.LoanService.Controllers.Dtos;

public class CheckLoanAvailableDto
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}
