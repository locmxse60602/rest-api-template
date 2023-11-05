using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Test> Tests { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
}
