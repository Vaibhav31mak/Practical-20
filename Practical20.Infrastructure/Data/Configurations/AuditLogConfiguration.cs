namespace Practical20.Infrastructure.Data.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.EntityName)
            .IsRequired()
            .HasMaxLength(128);
        builder.Property(a => a.ActionType)
            .IsRequired()
            .HasMaxLength(16);
        builder.Property(a => a.PrimaryKey)
            .HasMaxLength(128);
        builder.Property(a => a.Changes)
            .HasColumnType("nvarchar(max)");
    }
}
