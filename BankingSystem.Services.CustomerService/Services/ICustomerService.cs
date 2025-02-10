using System;
using BankingSystem.Services.CustomerService.Domain;

namespace BankingSystem.Services.CustomerService.Services;

public interface ICustomerService
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task UpdateAsync(Customer customer);
}
