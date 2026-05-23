namespace Practical20.Infrastructure.UnitOfWorkPattern;

public interface IUnitOfWork
{
    IBaseRepository<Student, Guid> Students { get; }
    Task<int> CommitAsync();
}
