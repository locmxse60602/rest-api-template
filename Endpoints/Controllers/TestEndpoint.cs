using Application.TestModule.Commands;
using Domain;
using Domain.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Controllers;

[ApiController]
[Route("tests")]
public class TestEndpoint : PublicControllerBase
{
    private readonly IMediator _mediator;

    public TestEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    public Task<BaseResult<Test>> Create([FromBody] CreateTestCommand request)
    {
        return _mediator.Send(request);
    }
}
