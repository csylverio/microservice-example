using System;
using BankingSystem.Services.NotificationWorker.Models;

namespace BankingSystem.Services.NotificationWorker.Services;

public class SmsService : ISmsService
{
    public async Task SendSmsAsync(SmsNotification smsNotification)
    {
        Console.WriteLine($"Sending SMS to {smsNotification.PhoneNumber}: {smsNotification.Message}");
    }
}
