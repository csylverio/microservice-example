using System;

namespace BankingSystem.Services.NotificationWorker.Models;

public class EmailNotification
{
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
}
