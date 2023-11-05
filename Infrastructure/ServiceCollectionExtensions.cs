// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net.Mime;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    // Adds the application module to the service collection.
    //
    // Parameters:
    //   services: The service collection to add the application module to.
    //
    // Returns:
    //   The service collection with the application module added.
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
