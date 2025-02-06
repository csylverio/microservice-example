using System;

namespace BankingSystem.Services.CustomerService.Services;

public interface ITokenService
{
    Task<string> GetTokenAsync(string login, string password);
}