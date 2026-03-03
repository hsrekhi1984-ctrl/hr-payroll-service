using HRPayroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRPayroll.Infrastructure.Persistence;

public sealed class PayrollDbContext(DbContextOptions<PayrollDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<PayrollRecord> PayrollRecords => Set<PayrollRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.EmployeeNumber).IsUnique();
            entity.Property(e => e.EmployeeNumber).HasMaxLength(32).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.BaseSalary).HasPrecision(18, 2);
            entity.Property(e => e.TaxRate).HasPrecision(5, 4);
        });

        modelBuilder.Entity<PayrollRecord>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.GrossPay).HasPrecision(18, 2);
            entity.Property(p => p.TaxAmount).HasPrecision(18, 2);
            entity.Property(p => p.NetPay).HasPrecision(18, 2);
            entity.Property(p => p.Currency).HasMaxLength(3);
            entity.HasOne(p => p.Employee)
                .WithMany(e => e.PayrollRecords)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
