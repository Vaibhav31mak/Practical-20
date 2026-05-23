namespace Practical20.Application.Contracts;

public interface IAuditLogService
{
    Task<Result<IReadOnlyList<AuditLogDto>>> GetAllAsync();
}
