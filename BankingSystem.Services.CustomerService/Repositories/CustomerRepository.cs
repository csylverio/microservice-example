using System.Data;
using BankingSystem.Services.CustomerService.Domain;
using Dapper;
using Npgsql;

namespace BankingSystem.Services.CustomerService.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Customer customer)
    {
        var query = @"INSERT INTO Customer (Id, Name, Email, DocumentNumber, PhoneNumber, Address, City, State, Country, PostalCode, CreatedAt, IsActive) 
                      VALUES (@Id, @Name, @Email, @DocumentNumber, @PhoneNumber, @Address, @City, @State, @Country, @PostalCode, @CreatedAt, @IsActive)";
        customer.Id = Guid.NewGuid();

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, new
        {
            customer.Id,
            customer.Name,
            customer.Email,
            customer.DocumentNumber,
            customer.PhoneNumber,
            customer.Address,
            customer.City,
            customer.State,
            customer.Country,
            customer.PostalCode,
            customer.CreatedAt,
            customer.IsActive
        });
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT Id, Name, Email, DocumentNumber, PhoneNumber, Address, City, State, Country, PostalCode, CreatedAt, UpdatedAt, IsActive FROM Customer WHERE Id = @Id";

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var query = @"SELECT Id, Name, Email, DocumentNumber, PhoneNumber, Address, City, State, Country, PostalCode, CreatedAt, UpdatedAt, IsActive FROM Customer";

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<Customer>(query);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var query = @"UPDATE Customer 
                     SET Name = @Name, 
                         Email = @Email, 
                         DocumentNumber = @DocumentNumber, 
                         PhoneNumber = @PhoneNumber, 
                         Address = @Address, 
                         City = @City, 
                         State = @State, 
                         Country = @Country, 
                         PostalCode = @PostalCode,
                         UpdatedAt = @UpdatedAt,
                         IsActive = @IsActive
                     WHERE Id = @Id";

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = @"DELETE FROM Customer WHERE Id = @Id";

        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}