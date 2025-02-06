using System;
using BankingSystem.Services.CustomerService.Domain;

namespace BankingSystem.Services.CustomerService.Repositories;

public interface IUserRepository
{
    Task<User?> GetByLoginAsync(string login);
}
