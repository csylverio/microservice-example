using System;
using BankingSystem.Services.CustomerService.Domain;
using BankingSystem.Services.CustomerService.Repositories;

namespace BankingSystem.Services.CustomerService.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly INotificationService _notificationService;

    public CustomerService(ICustomerRepository customerRepository, INotificationService notificationService)
    {
        _customerRepository = customerRepository;
        _notificationService = notificationService;
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
        await _notificationService.SendEmailAsync(emailNotification);

        // dispara notificação de sms
        SmsNotification smsNotification = new()
        {
            PhoneNumber = customer.PhoneNumber,
            Message = $"Olá {customer.Name}, seu cadastro foi realizado com sucesso. Seja bem-vindo(a) ao nosso banco."
        };
        await _notificationService.SendSmsAsync(smsNotification);

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
        await _notificationService.SendEmailAsync(emailNotification);

        // dispara notificação de sms
        SmsNotification smsNotification = new()
        {
            PhoneNumber = customer.PhoneNumber,
            Message = $"Olá {customer.Name}, seu cadastro foi atualizado com sucesso!."
        };
        await _notificationService.SendSmsAsync(smsNotification);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}
