// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Domain.Entities;

public abstract class EntityBaseEntityBase : IEntityBase
{
    public string Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
