// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Domain.Entities;

public abstract class EntityBase : IEntityBase
{
    public string Id { get; set; }
}
