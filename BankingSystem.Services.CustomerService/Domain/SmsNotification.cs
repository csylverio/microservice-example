using System;
using System.Text.Json;

namespace BankingSystem.Services.CustomerService.Domain;

// Trocar para Shared Libraries
public class SmsNotification
{
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

        public string ToJson()
        {
                return JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                        WriteIndented = true, // Formata o JSON com identação para melhor legibilidade
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Usa camelCase para as propriedades
                });
        }
}

