namespace BankingSystem.Services.NotificationWorker.Domain.Email;

public enum EmailNotificationType
{

    NewCustomerRegistration = 1,
    UpdatingCustomerData = 2,
    ClosingCustomerAccount = 3,
    DepositMadeSuccessfully = 4,
    WithdrawalMadeSuccessfully = 5,
    TransferMadeSuccessfully = 6,
    TransferReceivedSuccessfully = 7
}
