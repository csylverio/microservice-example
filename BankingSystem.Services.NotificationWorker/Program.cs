using BankingSystem.Services.NotificationWorker.Consumers;
using BankingSystem.Services.NotificationWorker.Services;
using BankingSystem.Services.NotificationWorker.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<ISmsService, SmsService>();

builder.Services.AddSingleton<IEmailNotificationConsumer, EmailNotificationConsumer>();
builder.Services.AddSingleton<ISmsNotificationConsumer, SmsNotificationConsumer>();

builder.Services.AddHostedService<EmailNotificationWorker>();
builder.Services.AddHostedService<SmsNotificationWorker>();


var host = builder.Build();
host.Run();
