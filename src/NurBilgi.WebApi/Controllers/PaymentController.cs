using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.Payments.Commands.CreatePayment;
using NurBilgi.Application.Features.Payments.Commands.RefundPayment;

namespace NurBilgi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize] // Adjust authorization as needed
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result.Success)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpPost("refund")]
        [Authorize] // Adjust authorization as needed
        public async Task<IActionResult> RefundPayment([FromBody] RefundPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result.Success)
                return BadRequest(result);
                
            return Ok(result);
        }
    }
} 