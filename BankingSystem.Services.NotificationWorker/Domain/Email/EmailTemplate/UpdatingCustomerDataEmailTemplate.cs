using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

public class UpdatingCustomerDataEmailTemplate : EmailTemplate
{
    private readonly EmailNotification _emailNotification;
    public UpdatingCustomerDataEmailTemplate(EmailNotification emailNotification)
    {
        To = emailNotification.EmailAddress;
        _emailNotification = emailNotification;
    }

    protected override async Task CreateBodyAsync()
    {
        Body = $"Olá {_emailNotification.CustomerName }! Seus dados foram atualizados com sucesso!";
    }

    protected override async Task CreateSubjectAsync()
    {
        Subject = $"Bem-vindo ao nosso banco!";
    }
}
