using System;

namespace BankingService.Services.LoanService.Controllers.Dtos;

public class RequestLoanDto
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}
