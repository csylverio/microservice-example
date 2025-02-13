using System;

namespace BankingSystem.Services.AccountService.Domain;

public class Transaction
{
    public Guid Id { get; private set; }
    public decimal Amount { get;  private set;}
    public string Description { get;  private set;}
    public TransactionType Type { get; private set;} 
    public DateTime Date { get; private set; }

    public Transaction(decimal amount, TransactionType type, string description) {
        Id = Guid.NewGuid();
        Amount = amount;
        Type = type;
        Description = description;
        Date = DateTime.Now;
    }
}
