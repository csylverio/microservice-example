using System;

namespace BankingSystem.Services.CustomerService.Services;

public interface IMessagerService
{
    Task PublishMessageAsync(string queue, string body);
}
