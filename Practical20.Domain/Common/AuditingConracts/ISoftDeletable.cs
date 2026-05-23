namespace Practical20.Domain.Common.AuditingConracts;

// SoftDeletable interface defines the properties that an entity must have to be considered
// soft deletable following ISP and LSP.
public interface ISoftDeletable
{
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
