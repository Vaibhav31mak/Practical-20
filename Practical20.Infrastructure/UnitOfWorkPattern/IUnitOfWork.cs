namespace Practical20.Infrastructure.UnitOfWorkPattern;

public interface IUnitOfWork
{
    IBaseRepository<Student, Guid> Students { get; }
    IBaseRepository<AuditLog, Guid> AuditLogs { get; }
    Task<int> CommitAsync();
}
