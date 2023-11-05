// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Domain;
using Domain.Common.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.TestModule.Commands;

public class CreateTestCommand : IRequest<BaseResult<Test>>
{
    public CreateTestCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, BaseResult<Test>>
{
    public Task<BaseResult<Test>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
