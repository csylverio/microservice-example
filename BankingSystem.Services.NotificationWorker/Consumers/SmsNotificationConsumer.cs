using System;
using System.Text;
using System.Text.Json;
using BankingSystem.Services.NotificationWorker.Models;
using BankingSystem.Services.NotificationWorker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BankingSystem.Services.NotificationWorker.Consumers;

public class SmsNotificationConsumer : ISmsNotificationConsumer
{
    private readonly ISmsService _smsService;
    private const string _queueName = "sms_notifications";

    public SmsNotificationConsumer(ISmsService smsService)
    {
        _smsService = smsService;
    }

    public async Task StartListeningAsync(CancellationToken stoppingToken)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "rabbitmq", Password = "102030", Port = 5672 };
            using var connection = await factory.CreateConnectionAsync(stoppingToken);
            using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var smsNotification = JsonSerializer.Deserialize<SmsNotification>(message);

                    if (smsNotification == null || string.IsNullOrEmpty(smsNotification.PhoneNumber) ||
                        string.IsNullOrEmpty(smsNotification.Message))
                    {
                        Console.WriteLine("Error in SmsNotificationConsumer ReceivedAsync: SmsNotification is null");
                        return;
                    }

                    await _smsService.SendSmsAsync(smsNotification);

                    // Confirma manualmente a mensagem após o processamento bem-sucedido
                    //await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Failed to deserialize SmsNotification: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in SmsNotificationConsumer ReceivedAsync: {ex.Message}");
                }
            };

            // autoAck: true significa que a mensagem será automaticamente confirmada assim que for recebida, mesmo que ocorra um erro no processamento.
            await channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("SmsNotificationConsumer was canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SmsNotificationConsumer StartListeningAsync: {ex.Message}");
        }
    }
}
