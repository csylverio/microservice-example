using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Sms;

public class SmsNotification
{
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
}
