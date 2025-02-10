using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email;

public class Email
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
