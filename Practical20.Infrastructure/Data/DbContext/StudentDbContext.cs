using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Practical20.Infrastructure.Data.DbContext;

// This class represents the DbContext for the application.
public partial class StudentDbContext(DbContextOptions<StudentDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // SaveChanges and SaveChangesAsync are overridden to apply auditing and logging
    // logic before saving changes to the database. This is the best practice to ensure
    // that all changes are audited and logged consistently across the application.
    public override int SaveChanges()
    {
        ApplyAuditingAndLogs();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditingAndLogs();
        return base.SaveChangesAsync(cancellationToken);
    }
}
