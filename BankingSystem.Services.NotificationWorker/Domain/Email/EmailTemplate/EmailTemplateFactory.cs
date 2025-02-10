using System;

namespace BankingSystem.Services.NotificationWorker.Domain.Email.EmailTemplate;

public class EmailTemplateFactory
{
    internal static IEmailTemplate Create(EmailNotification emailNotification)
    {
        switch(emailNotification.Type)
        {
            case EmailNotificationType.NewCustomerRegistration:
                return new NewCustomerRegistrationEmailTemplate(emailNotification);

            case EmailNotificationType.UpdatingCustomerData:
                return new UpdatingCustomerDataEmailTemplate(emailNotification);

            default:
                throw new Exception("Invalid email notification type");
        }
    }
}
