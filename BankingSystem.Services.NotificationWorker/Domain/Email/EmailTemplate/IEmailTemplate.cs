using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

public interface IEmailTemplate
{
    Task<Email> GenerateAsync();
}
