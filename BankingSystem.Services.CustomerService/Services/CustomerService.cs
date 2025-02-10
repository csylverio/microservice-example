using System;
using BankingSystem.Services.CustomerService.Domain;
using BankingSystem.Services.CustomerService.Repositories;

namespace BankingSystem.Services.CustomerService.Services;

public class CustomerService : ICustomerService
{
    private const string _emailQueue = "email_notifications";
    private const string _smsQueue = "sms_notifications";
    private const string _accountQueue = "account_notifications";

    private readonly ICustomerRepository _customerRepository;
    private readonly IMessagerService _messagerService;

    public CustomerService(ICustomerRepository customerRepository, IMessagerService messagerService)
    {
        _customerRepository = customerRepository;
        _messagerService = messagerService;
    }

    public async Task AddAsync(Customer customer)
    {
        await _customerRepository.AddAsync(customer);

        // dispara notificação de email
        EmailNotification emailNotification = new()
        {
            EmailAddress = customer.Email,
            CustomerName = customer.Name,
            Type = 1
        };
        await _messagerService.PublishMessageAsync(_emailQueue, emailNotification.ToJson());

        // dispara notificação de sms
        SmsNotification smsNotification = new()
        {
            PhoneNumber = customer.PhoneNumber,
            Message = $"Olá {customer.Name}, seu cadastro foi realizado com sucesso. Seja bem-vindo(a) ao nosso banco."
        };
        await _messagerService.PublishMessageAsync(_smsQueue, smsNotification.ToJson());

        // dispara notificação para criar conta
        AccountNotification accountNotification = new()
        {
            AccountNumber = customer.Id.ToString()
        };
        await _messagerService.PublishMessageAsync(_accountQueue, accountNotification.ToJson());
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _customerRepository.UpdateAsync(customer);

        // dispara notificação de email
        EmailNotification emailNotification = new()
        {
            EmailAddress = customer.Email,
            CustomerName = customer.Name,
            Type = 2
        };
        await _messagerService.PublishMessageAsync(_emailQueue, emailNotification.ToJson());

        // dispara notificação de sms
        SmsNotification smsNotification = new()
        {
            PhoneNumber = customer.PhoneNumber,
            Message = $"Olá {customer.Name}, seu cadastro foi atualizado com sucesso!."
        };
        await _messagerService.PublishMessageAsync(_smsQueue, smsNotification.ToJson());
    }

    public async Task DeleteAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}
