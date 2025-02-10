using System;
using System.Text;
using BankingSystem.Services.CustomerService.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace BankingSystem.Services.CustomerService.Services;

public class NotificationService : INotificationService
{
    private const string _queueEmail = "email_notifications";
    private const string _queueSms = "sms_notifications";

    public async Task SendEmailAsync(EmailNotification emailNotification)
    {
        await PublishMessageAsync(_queueEmail, emailNotification.ToJson());
    }

    public async Task SendSmsAsync(SmsNotification smsNotification)
    {
        await PublishMessageAsync(_queueSms, smsNotification.ToJson());
    }

    private static async Task PublishMessageAsync(string queue, string body)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "rabbitmq", Password = "102030", Port = 5672 };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(body);
            await channel.BasicPublishAsync("", queue, false,  new BasicProperties(), messageBodyBytes);
        }
        catch (PublishException ex)
        {
            Console.WriteLine($"Erro na publicação com o RabbitMQ. Message: {ex.Message}");
        }
    }
}
