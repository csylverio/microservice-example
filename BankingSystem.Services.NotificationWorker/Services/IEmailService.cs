using System;
using BankingSystem.Services.NotificationWorker.Models;

namespace BankingSystem.Services.NotificationWorker.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailNotification email);
}
