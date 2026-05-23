namespace Practical20.Domain.Common.AuditingEntities;

// This is the base entity class which implements all the auditing properties and
// also the concurrency check property. Followed ISP and LSP by making separate
// interfaces for each concern and then implementing them in the base entity class.
public class BaseEntity<TId> : ICreatable,
    IUpdatable,
    ISoftDeletable,
    IConcurrencyCheck,
    IEntity<TId> where TId : struct
{
    public TId Id { get; init; } = default!;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public byte[] RowVersion { get; set; } = [];
}
