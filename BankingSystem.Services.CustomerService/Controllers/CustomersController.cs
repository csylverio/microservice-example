using BankingSystem.Services.CustomerService.Dtos;
using BankingSystem.Services.CustomerService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Services.CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto customerDto)
    {
        var customer = customerDto.ToCustomer();
        await _customerRepository.AddAsync(customer);
        return Ok(customer);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return Ok(customers);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateCustomerDto customerDto)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(id);
        if (existingCustomer == null)
            return NotFound();

        var customer = customerDto.ToCustomer();
        customer.Id = id;
        customer.CreatedAt = existingCustomer.CreatedAt;
        customer.UpdatedAt = DateTime.UtcNow;
        await _customerRepository.UpdateAsync(customer);
        return Ok(customer);
    }
}