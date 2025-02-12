using System;
using System.Text;
using System.Text.Json;
using BankingSystem.Services.CustomerService.Domain;
using BankingSystem.Services.CustomerService.Repositories;

namespace BankingSystem.Services.CustomerService.Services;

public class CustomerService : ICustomerService
{
    // adicionar configurações no appsettings.json
    private const string _emailQueue = "email_notifications";
    private const string _smsQueue = "sms_notifications";
    private const string accountServiceUrl = "http://account-service:5000";

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
        Console.WriteLine("Disparando notificação de email - Customer Create");
        EmailNotification emailNotification = new()
        {
            EmailAddress = customer.Email,
            CustomerName = customer.Name,
            Type = 1
        };
        await _messagerService.PublishMessageAsync(_emailQueue, emailNotification.ToJson());

        // dispara notificação de sms
        Console.WriteLine("Disparando notificação de sms - Customer Create");
        SmsNotification smsNotification = new()
        {
            PhoneNumber = customer.PhoneNumber,
            Message = $"Olá {customer.Name}, seu cadastro foi realizado com sucesso. Seja bem-vindo(a) ao nosso banco."
        };
        await _messagerService.PublishMessageAsync(_smsQueue, smsNotification.ToJson());

        // realiza chamada HTTP para API Account Service
        Console.WriteLine("Criando conta para o cliente - API Account Service");
        var requestBody = new
        {
            accountNumber = customer.Id.ToString(),
            initialBalance = 0
        };
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsync($"{accountServiceUrl}/api/Account", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Erro ao criar conta para o cliente. StatusCode:{response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }
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
        Console.WriteLine("Disparando notificação de email - Customer Update");
        EmailNotification emailNotification = new()
        {
            EmailAddress = customer.Email,
            CustomerName = customer.Name,
            Type = 2
        };
        await _messagerService.PublishMessageAsync(_emailQueue, emailNotification.ToJson());

        // dispara notificação de sms
        Console.WriteLine("Disparando notificação de sms - Customer Update");
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
