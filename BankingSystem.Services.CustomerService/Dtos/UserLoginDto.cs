using System;
using BankingSystem.Services.CustomerService.Domain;

namespace BankingSystem.Services.CustomerService.Dtos;

public class UserLoginDto
{
    public string Login { get; set; }
    public string Password { get; set; }

    public User ToUser()
    {
        return new User()
        {
            Login = Login,
            Password = Password
        };
    }
}
