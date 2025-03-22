using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;
using NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

namespace NurBilgi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiChatMessagesController : ControllerBase
    {
        private readonly ISender _mediator;

        public AiChatMessagesController(ISender mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<AiChatMessageGetAllDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllAiChatMessagesQuery(
                string.Empty, 
                false, 
                DateTimeOffset.UtcNow, 
                0 // Default customer ID, replace with actual value if needed
            );
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AiChatMessageGetByIdDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var query = new AiChatMessageGetByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}