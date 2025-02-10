using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

public class NewCustomerRegistrationEmailTemplate : EmailTemplate
{
    private readonly EmailNotification _emailNotification;
    public NewCustomerRegistrationEmailTemplate(EmailNotification emailNotification)
    {
        To = emailNotification.EmailAddress;
        _emailNotification = emailNotification;
    }

    protected override async Task CreateBodyAsync()
    {
        Body = $"Bem-vindo ao nosso banco! Estamos felizes em ter você {_emailNotification.CustomerName} como nosso cliente.";
    }

    protected override async Task CreateSubjectAsync()
    {
        Subject = "Bem-vindo ao nosso banco!";
    }
}
