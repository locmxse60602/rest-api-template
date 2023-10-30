using System.Text.Json;
using Domain;
using Domain.Interfaces;
using Infrastructure.DbConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Test> Tests { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTypeConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // Performs an audit of the changes made in the current context.
    // This method asynchronously generates an audit for each entity that has been added, modified, or deleted.
    // The audit includes incrementing the version number of the entity and creating an audit record if the entity implements the IAuditable interface.
    // The audit is performed using the current date and time in UTC format.
    // This method returns a Task representing the asynchronous operation.
    private async Task AuditChanges()
    {
        var now = DateTime.UtcNow;

        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added ||
                        x.State == EntityState.Modified ||
                        x.State == EntityState.Deleted).ToList();

        foreach (var entityEntry in entityEntries)
        {
            IncrementVersionNumber(entityEntry);
            if (entityEntry.Entity is IAuditable) await CreateAuditAsync(entityEntry, now);
        }
    }

    // Creates an audit log for the given entity entry and timestamp.
    // 
    // Parameters:
    //   entityEntry: The EntityEntry object representing the entity being audited.
    //   timeStamp: The timestamp of the audit log creation.
    //
    // Returns:
    //   A Task representing the asynchronous operation.
    private async Task CreateAuditAsync(EntityEntry entityEntry, DateTime timeStamp)
    {
        if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Deleted)
        {
            var changeLog = new AuditLog
            {
                EntityName = entityEntry.Entity.GetType().Name,
                Action = entityEntry.State.ToString(),
                TimeStamp = timeStamp
            };
            await AuditLogs.AddAsync(changeLog);
        }
        else
        {
            var originalValues = new List<AuditLogValue>();
            var currentValues = new List<AuditLogValue>();
            foreach (var prop in entityEntry.OriginalValues.Properties)
            {
                var originalValue = !string.IsNullOrWhiteSpace(entityEntry.OriginalValues[prop]?.ToString())
                    ? entityEntry.OriginalValues[prop]?.ToString()
                    : null;

                var currentValue = !string.IsNullOrWhiteSpace(entityEntry.CurrentValues[prop]?.ToString())
                    ? entityEntry.CurrentValues[prop]?.ToString()
                    : null;

                if (originalValue == currentValue) continue;

                originalValues.Add(new AuditLogValue(prop.Name, originalValue));
                currentValues.Add(new AuditLogValue(prop.Name, currentValue));
            }

            var changeLog = new AuditLog
            {
                EntityName = entityEntry.Entity.GetType().Name,
                Action = entityEntry.State.ToString(),
                TimeStamp = timeStamp,
                OldValues = JsonSerializer.Serialize(originalValues),
                NewValues = JsonSerializer.Serialize(currentValues)
            };
            await AuditLogs.AddAsync(changeLog);
        }
    }

    // Increments the version number of an entity by 1.
    //
    // Parameters:
    //   entityEntry: The entity entry to increment the version number for.
    private static void IncrementVersionNumber(EntityEntry entityEntry)
    {
        if (entityEntry.Metadata.FindProperty("Version") == null) return;
        var currentVersionNumber = Convert.ToInt32(entityEntry.CurrentValues["Version"]);
        entityEntry.CurrentValues["Version"] = currentVersionNumber + 1;
    }
}