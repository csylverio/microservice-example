using System;
using BankingSystem.Services.NotificationWorker.Domain.Email;
using BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

namespace BankingSystem.Services.NotificationWorker.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(EmailNotification emailNotification)
    {
        try
        {
            IEmailTemplate emailTemplate = EmailTemplateFactory.Create(emailNotification);
            Email email = await emailTemplate.GenerateAsync();

            // Simulação de envio de e-mail (substituir por implementação real)
            Console.WriteLine($"Enviando e-mail para {email.To}: {email.Subject}");
            Console.WriteLine($"Corpo do e-mail: {email.Body}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}
