using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace BankingSystem.Services.CustomerService.Services;

public class MessagerService : IMessagerService
{
    public MessagerService()
    {
    }

    public async Task PublishMessageAsync(string queue, string body)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "rabbitmq", Password = "102030", Port = 5672 };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(body);
            await channel.BasicPublishAsync("", queue, false, new BasicProperties(), messageBodyBytes);
        }
        catch (PublishException ex)
        {
            Console.WriteLine($"Erro na publicação com o RabbitMQ. Message: {ex.Message}");
        }
    }
}
