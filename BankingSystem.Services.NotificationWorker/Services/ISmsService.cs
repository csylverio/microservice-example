using BankingSystem.Services.NotificationWorker.Models;

namespace BankingSystem.Services.NotificationWorker.Services;

public interface ISmsService
{
    Task SendSmsAsync(SmsNotification smsNotification);
}
