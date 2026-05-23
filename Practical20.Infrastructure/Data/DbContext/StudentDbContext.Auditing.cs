using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Practical20.Infrastructure.Data.DbContext;

/// <summary>
/// This is the partial class for StudentDbContext that contains the logic for auditing 
/// and logging changes to the entities. This makes the code of StudentDBContext cleaner.
/// </summary>
public partial class StudentDbContext
{
    private void ApplyAuditingAndLogs()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is AuditLog)
                continue;
            if (entry.State == EntityState.Added && entry.Entity is ICreatable creatable)
            {
                entry.Property(nameof(ICreatable.CreatedAt)).CurrentValue = now;
            }

            if (entry.State == EntityState.Modified && entry.Entity is IUpdatable)
            {
                entry.Property(nameof(IUpdatable.UpdatedAt)).CurrentValue = now;
                AddAuditLog(entry, "Update", now);
            }

            // We insert into audit log if its not soft deletable, otherwise we just mark it
            // as deleted and set the deleted at timestamp.
            if (entry.State == EntityState.Deleted)
            {
                if (entry.Entity is ISoftDeletable)
                {
                    entry.State = EntityState.Modified;
                    entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                    entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = now;
                }
                else
                {
                    AddAuditLog(entry, "Delete", now);
                }
            }
        }
    }

    private void AddAuditLog(EntityEntry entry, string actionType, DateTimeOffset now)
    {
        var primaryKey = entry.Metadata.FindPrimaryKey()?.Properties
            .Select(p => entry.Property(p.Name).CurrentValue?.ToString())
            .FirstOrDefault();
        var changes = entry.Properties
            .Where(p => p.IsModified || entry.State == EntityState.Deleted)
            .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue?.ToString());
        var log = new AuditLog
        {
            EntityName = entry.Metadata.ClrType.Name,
            ActionType = actionType,
            OccurredAt = now,
            PrimaryKey = primaryKey,
            Changes = changes.Count > 0 ? JsonSerializer.Serialize(changes) : null
        };

        AuditLogs.Add(log);
    }
}
