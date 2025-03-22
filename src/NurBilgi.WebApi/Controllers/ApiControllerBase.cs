using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NurBilgi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender Mediator;
    protected ApiControllerBase(ISender mediator)
    {
        Mediator = mediator;
    }
}