using System;
using BankingSystem.Services.AccountService.Domain;
using MongoDB.Driver;

namespace BankingSystem.Services.AccountService.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IMongoCollection<Account> _accountsCollection;

    public AccountRepository(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _accountsCollection = database.GetCollection<Account>("Accounts");
    }

    public async Task<Account> GetByAccountNumberAsync(string accountNumber)
    {
        return await _accountsCollection
            .Find(account => account.AccountNumber == accountNumber)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Account account)
    {
        await _accountsCollection.InsertOneAsync(account);
    }

    public async Task UpdateBalanceAsync(Account account)
    {
        var update = Builders<Account>.Update.Set(a => a.Balance, account.Balance)
                                             .Set(a => a.Transactions, account.Transactions);
        await _accountsCollection.UpdateOneAsync(
            objAccount => objAccount.AccountNumber == account.AccountNumber,
            update
        );
    }

    public async Task TransferAsync(Account fromAccount, Account toAccount)
    {
        using var session = await _accountsCollection.Database.Client.StartSessionAsync();

        // session.StartTransaction();
        // try
        // {

        await UpdateBalanceAsync(fromAccount);
        await UpdateBalanceAsync(toAccount);

        //  await session.CommitTransactionAsync();
        // }
        // catch
        // {
        //     await session.AbortTransactionAsync();
        //     throw;
        // }
    }
}