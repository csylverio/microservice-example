using System;
using System.Text.Json;

namespace BankingSystem.Services.CustomerService.Domain;

// Trocar para Shared Libraries
public class EmailNotification
{
        public string EmailAddress { get; set; }
        public string CustomerName { get; set; }
        public int Type { get; set; }

        public string ToJson()
        {
                return JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                        WriteIndented = true, // Formata o JSON com identação para melhor legibilidade
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Usa camelCase para as propriedades
                });
        }
}
