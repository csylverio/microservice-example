using System;
using System.Text.Json;

namespace BankingSystem.Services.CustomerService.Domain;

public class AccountNotification
{
    public string AccountNumber { get; set; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true, // Formata o JSON com identação para melhor legibilidade
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Usa camelCase para as propriedades
        });
    }
}
