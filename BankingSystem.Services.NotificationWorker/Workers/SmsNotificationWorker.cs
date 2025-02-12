using System;
using BankingSystem.Services.NotificationWorker.Consumers;

namespace BankingSystem.Services.NotificationWorker.Workers;

public class SmsNotificationWorker : BackgroundService
{
    private readonly ILogger<SmsNotificationWorker> _logger;
    private readonly ISmsNotificationConsumer _smsNotificationConsumer;

    public SmsNotificationWorker(ILogger<SmsNotificationWorker> logger, ISmsNotificationConsumer smsNotificationConsumer)
    {
        _logger = logger;
        _smsNotificationConsumer = smsNotificationConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // _logger.LogInformation("SmsNotificationWorker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
            await _smsNotificationConsumer.StartListeningAsync(stoppingToken);
        }
    }
}
