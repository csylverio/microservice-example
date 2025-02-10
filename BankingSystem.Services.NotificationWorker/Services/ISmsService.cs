using BankingSystem.Services.NotificationWorker.Domain.Sms;

namespace BankingSystem.Services.NotificationWorker.Services;

public interface ISmsService
{
    Task SendSmsAsync(SmsNotification smsNotification);
}
