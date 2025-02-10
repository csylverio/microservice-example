using BankingSystem.Services.CustomerService.Dtos;
using BankingSystem.Services.CustomerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Services.CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto customerDto)
    {
        var customer = customerDto.ToCustomer();
        await _customerService.AddAsync(customer);
        return Ok(customer);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateCustomerDto customerDto)
    {
        var existingCustomer = await _customerService.GetByIdAsync(id);
        if (existingCustomer == null)
            return NotFound();

        var customer = customerDto.ToCustomer(existingCustomer);
        await _customerService.UpdateAsync(customer);
        return Ok(customer);
    }
}