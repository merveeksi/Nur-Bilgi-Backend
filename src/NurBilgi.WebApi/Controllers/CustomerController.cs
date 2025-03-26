using MediatR;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.Customers.Commands.Create;
using NurBilgi.Application.Features.Customers.Commands.Delete;
using NurBilgi.Application.Features.Customers.Commands.Update;
using NurBilgi.Application.Features.Customers.Queries.GetAll;
using NurBilgi.Application.Features.Customers.Queries.GetById;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.WebApi.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ApiControllerBase
{
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ISender mediator, ILogger<CustomerController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string searchTerm = "", CancellationToken cancellationToken = default)
    {
        var query = new GetAllCustomersQuery(searchTerm);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var query = new CustomerGetByIdQuery(id);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Id in URL doesn't match Id in request body");
            
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id);
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("sample")]
    public IActionResult GetSampleCustomers()
    {
        // Creating two sample customers with valid data
        var customer1 = new
        {
            Id = 1,
            UserName = "ahmet123",
            FullName = "Ahmet Yılmaz",
            Email = "ahmet.yilmaz@example.com",
            Password = "Test1234!"
        };
        
        var customer2 = new
        {
            Id = 2,
            UserName = "ayse456",
            FullName = "Ayşe Demir",
            Email = "ayse.demir@example.com",
            Password = "Test5678!"
        };
        
        return Ok(new[] { customer1, customer2 });
    }
} 