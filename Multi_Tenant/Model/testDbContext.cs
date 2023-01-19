using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Multi_Tenant.Model
{
    public partial class testDbContext : DbContext
    {
        public testDbContext()
        {
        }

        public testDbContext(DbContextOptions<testDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDetail> AccountDetails { get; set; } = null!;
        public virtual DbSet<AccountHolder> AccountHolders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=testDb;Trusted_Connection=True;");
            }
        }

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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
