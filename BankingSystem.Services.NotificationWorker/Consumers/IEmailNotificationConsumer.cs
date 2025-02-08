using System;

namespace BankingSystem.Services.NotificationWorker.Consumers;

public interface IEmailNotificationConsumer
{
    Task StartListeningAsync(CancellationToken stoppingToken);
}
