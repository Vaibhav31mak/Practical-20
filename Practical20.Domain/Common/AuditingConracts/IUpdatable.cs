namespace Practical20.Domain.Common.AuditingConracts;

public interface IUpdatable
{
    public DateTimeOffset UpdatedAt { get; set; }
}
