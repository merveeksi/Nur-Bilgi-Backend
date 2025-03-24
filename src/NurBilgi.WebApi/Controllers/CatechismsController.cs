using MediatR;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Application.Features.Catechisms.Commands.Create;
using NurBilgi.Application.Features.Catechisms.Commands.Delete;
using NurBilgi.Application.Features.Catechisms.Commands.Update;
using NurBilgi.Application.Features.Catechisms.Queries.GetAll;
using NurBilgi.Application.Features.Catechisms.Queries.GetById;

namespace NurBilgi.WebApi.Controllers;

public class CatechismsController : ApiControllerBase
{
    public CatechismsController(ISender mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<CatechismGetAllDto>>> GetAllAsync(
        [FromQuery] string bookName = "", 
        [FromQuery] string authorName = "",
        [FromQuery] string tags = "",
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCatechismsQuery(bookName, authorName, tags);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatechismGetByIdDto>> GetByIdAsync(
        long id, 
        CancellationToken cancellationToken = default)
    {
        var query = new CatechismGetByIdQuery(id);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<long>>> CreateAsync(
        [FromBody] CreateCatechismCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<long>>> UpdateAsync(
        long id,
        [FromBody] UpdateCatechismCommand command,
        CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<long>>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCatechismCommand(id);
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
} 