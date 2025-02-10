using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

public abstract class EmailTemplate : IEmailTemplate
{
    protected string To { get; set; }
    protected string Body { get; set; }
    protected string Subject { get; set; }
    protected abstract Task CreateBodyAsync();
    protected abstract Task CreateSubjectAsync();

    public async Task<Email> GenerateAsync()
    {
        await CreateBodyAsync();
        await CreateSubjectAsync();

        return new Email
        {
            To = To,
            Body = Body,
            Subject = Subject
        };
    }
}
