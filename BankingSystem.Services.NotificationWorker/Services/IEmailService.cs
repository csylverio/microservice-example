using System;
using BankingSystem.Services.NotificationWorker.Domain.Email;

namespace BankingSystem.Services.NotificationWorker.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailNotification email);
}
