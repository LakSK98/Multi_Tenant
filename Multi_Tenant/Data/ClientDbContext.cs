using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Multi_Tenant.Model;
using Multi_Tenant.Models;
using System.Data;

namespace Multi_Tenant.Data
{
    public partial class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions options) : base(options) {
        }
        
        public DbSet<AccountDetail> AccountDetails { get; set; }
        public DbSet<AccountHolder> AccountHolders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetail>(entity =>
            {
                entity.HasKey(e => e.AccountNo)
                    .HasName("PK__AccountD__349D9DFD73712313");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AccountType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountHolder)
                    .WithMany(p => p.AccountDetails)
                    .HasForeignKey(d => d.AccountHolderId)
                    .HasConstraintName("FK_AccountDetails");
            });

            modelBuilder.Entity<AccountHolder>(entity =>
            {
                entity.ToTable("AccountHolder");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
