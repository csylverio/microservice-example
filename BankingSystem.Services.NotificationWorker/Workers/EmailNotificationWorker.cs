using BankingSystem.Services.NotificationWorker.Consumers;

namespace BankingSystem.Services.NotificationWorker.Workers;

public class EmailNotificationWorker : BackgroundService
{
    private readonly ILogger<EmailNotificationWorker> _logger;
    private readonly IEmailNotificationConsumer _emailNotificationConsumer;

    public EmailNotificationWorker(ILogger<EmailNotificationWorker> logger, IEmailNotificationConsumer emailNotificationConsumer)
    {
        _logger = logger;
        _emailNotificationConsumer = emailNotificationConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // _logger.LogInformation("EmailNotificationWorker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
            await _emailNotificationConsumer.StartListeningAsync(stoppingToken);
        }
    }
}
