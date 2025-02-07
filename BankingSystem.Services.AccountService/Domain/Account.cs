using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankingSystem.Services.AccountService.Domain;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public Account(string accountNumber, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        Transactions = [new Transaction(initialBalance, TransactionType.OpeningBalance, "Saldo inicial")];
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new InvalidOperationException("O valor do depósito deve ser positivo.");
        Balance += amount;
        Transactions.Add(new Transaction(amount, TransactionType.Deposit, "Depósito"));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new InvalidOperationException("O valor do saque deve ser positivo.");
        if (amount > Balance) throw new InvalidOperationException("Saldo insuficiente.");
        Balance -= amount;
        Transactions.Add(new Transaction(-amount, TransactionType.Withdrawal, "Saque"));
    }
}

