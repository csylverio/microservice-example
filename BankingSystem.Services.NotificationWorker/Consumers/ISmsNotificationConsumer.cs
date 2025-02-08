using System;

namespace BankingSystem.Services.NotificationWorker.Consumers;

public interface ISmsNotificationConsumer
{
    Task StartListeningAsync(CancellationToken stoppingToken);
}
