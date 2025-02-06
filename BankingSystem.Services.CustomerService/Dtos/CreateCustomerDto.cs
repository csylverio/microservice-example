using System;
using BankingSystem.Services.CustomerService.Domain;

namespace BankingSystem.Services.CustomerService.Dtos;

public class CreateCustomerDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }

    internal Customer ToCustomer()
    {
        return new Customer
        {
            Name = Name,
            Email = Email,
            DocumentNumber = DocumentNumber,
            PhoneNumber = PhoneNumber,
            Address = Address,
            City = City,
            State = State,
            Country = Country,
            PostalCode = PostalCode
        };
    }
}
