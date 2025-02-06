using System.Data;
using BankingSystem.Services.CustomerService.Domain;
using Dapper;
using Npgsql;

namespace BankingSystem.Services.CustomerService.Repositories;

public class UserRepository(string connectionString) : IUserRepository
{
    private readonly string _connectionString = connectionString;

    public async Task<User?> GetByLoginAsync(string login)
    {
        const string query = @"SELECT id, ""name"", email, login, ""password"", ""role"", createdat, updatedat, isactive
                               FROM appuser 
                               WHERE Login = @Login";

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Login = login });
    }
}