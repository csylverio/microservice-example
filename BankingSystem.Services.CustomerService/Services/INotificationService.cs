using System;
using BankingSystem.Services.CustomerService.Domain;

namespace BankingSystem.Services.CustomerService.Services;

public interface INotificationService
{
    Task SendEmailAsync(EmailNotification emailNotification);
    Task SendSmsAsync(SmsNotification smsNotification);
}
