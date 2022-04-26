using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.Models
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public virtual DbSet<Employees> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeName)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeName").IsRequired();

                entity.Property(e => e.EmployeeAddress)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.EmployeeAddress)
                    .IsRequired()
                    .HasMaxLength(100).IsFixedLength();

            });

            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
