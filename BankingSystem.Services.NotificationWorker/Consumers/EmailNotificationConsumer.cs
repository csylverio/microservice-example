using System.Text;
using System.Text.Json;
using BankingSystem.Services.NotificationWorker.Domain.Email;
using BankingSystem.Services.NotificationWorker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BankingSystem.Services.NotificationWorker.Consumers;

public class EmailNotificationConsumer : IEmailNotificationConsumer
{
    private readonly IEmailService _emailService;
    private const string _queueName = "email_notifications";

    public EmailNotificationConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task StartListeningAsync(CancellationToken stoppingToken)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "rabbitmq", Password = "102030", Port = 5672 };
            using var connection = await factory.CreateConnectionAsync(stoppingToken);
            using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var emailNotification = JsonSerializer.Deserialize<EmailNotification>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Permite ignorar diferenças entre PascalCase e camelCase
                    });

                    if (emailNotification == null || string.IsNullOrEmpty(emailNotification.CustomerName) ||
                        string.IsNullOrEmpty(emailNotification.EmailAddress))
                    {
                        Console.WriteLine($"Invalid email notification received: {message}");
                        return;
                    }

                    await _emailService.SendEmailAsync(emailNotification);

                    // Confirma manualmente a mensagem após o processamento bem-sucedido
                    //await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Failed to deserialize EmailNotification: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in EmailNotificationConsumer ReceivedAsync: {ex.Message}");
                }
            };

            // autoAck: true significa que a mensagem será automaticamente confirmada assim que for recebida, mesmo que ocorra um erro no processamento.
            await channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("EmailNotificationConsumer was canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in EmailNotificationConsumer StartListeningAsync: {ex.Message}");
        }
    }
}
