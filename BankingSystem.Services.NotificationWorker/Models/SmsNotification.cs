using System;

namespace BankingSystem.Services.NotificationWorker.Models;

public class SmsNotification
{
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
}
