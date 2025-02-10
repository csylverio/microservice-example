using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email;

public class EmailNotification
{
        public string EmailAddress { get; set; }

        public string CustomerName { get; set; }

        public EmailNotificationType Type { get; set; }
}
