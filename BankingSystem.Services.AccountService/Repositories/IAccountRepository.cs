using System;
using BankingSystem.Services.AccountService.Domain;

namespace BankingSystem.Services.AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByAccountNumberAsync(string accountNumber);
    Task AddAsync(Account account);
    Task UpdateBalanceAsync(Account account);
    Task TransferAsync(Account fromAccount, Account toAccount);
}
