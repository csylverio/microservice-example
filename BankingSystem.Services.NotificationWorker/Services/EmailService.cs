using System;
using BankingSystem.Services.NotificationWorker.Models;

namespace BankingSystem.Services.NotificationWorker.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(EmailNotification email)
    {
        // Simulação de envio de e-mail (substituir por implementação real)
        Console.WriteLine($"Enviando e-mail para {email.To}: {email.Subject}");
        Console.WriteLine($"Corpo do e-mail: {email.Body}");
    }
}
