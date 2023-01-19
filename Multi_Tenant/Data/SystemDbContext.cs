using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Multi_Tenant.Models;

namespace Multi_Tenant.Data
{
    public partial class SystemDbContext : DbContext
    {
        public SystemDbContext()
        {
        }

        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseServer)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.TenantName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Tenant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
