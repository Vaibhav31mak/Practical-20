namespace Practical20.Application.Services;

public sealed class AuditLogService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<AuditLogService> logger) : IAuditLogService
{
    public async Task<Result<IReadOnlyList<AuditLogDto>>> GetAllAsync()
    {
        var logs = await unitOfWork.AuditLogs.GetAllAsync();
        logger.LogInformation("Fetched {AuditLogCount} audit logs", logs.Count);
        return Result<IReadOnlyList<AuditLogDto>>.Success(
            [.. logs.Select(mapper.Map<AuditLogDto>)]);
    }
}
