namespace Practical20.Application.Dtos.AuditLogs;

public sealed record AuditLogDto(
    Guid Id,
    string EntityName,
    string ActionType,
    DateTimeOffset OccurredAt,
    string? PrimaryKey,
    string? Changes
);
