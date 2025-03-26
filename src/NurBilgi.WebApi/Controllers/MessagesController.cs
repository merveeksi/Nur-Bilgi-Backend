using MediatR;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.Messages.Commands.Create;

namespace NurBilgi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly ISender _mediator;

    public MessagesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
} 