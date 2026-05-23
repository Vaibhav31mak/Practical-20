namespace Practical20.Domain.Entities;

public sealed class AuditLog : IEntity<Guid>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string EntityName { get; init; }
    public required string ActionType { get; init; }
    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;
    public string? PrimaryKey { get; init; }
    public string? Changes { get; init; }
}
