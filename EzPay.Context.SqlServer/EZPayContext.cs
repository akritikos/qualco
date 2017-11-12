namespace EzPay.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using EzPay.Model.Entities;
    using EzPay.Model.IdentityEntities;
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <inheritdoc cref="DbContext" />
    [SuppressMessage(
        "ReSharper",
        "InheritdocInvalidUsage",
        Justification = "Class is DbContext via inheritance from IdentityDbContext")]
    public sealed class EzPaySqlServerContext :
        IdentityDbContext<Citizen, Role, long, CitizenClaim, CitizenRole, CitizenLogin, RoleClaim, CitizenToken>,
        IEzPayRepository
    {
        /// <inheritdoc />
        public EzPaySqlServerContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        /// <inheritdoc />
        public EzPaySqlServerContext()
        {
            Database.Migrate();
        }

        #region IEzPayRepository
        /// <inheritdoc />
        public void ClearVolatile()
        {
            var query = Database.ExecuteSqlCommand(
                $"SELECT COUNT(*) FROM [sys].[objects] WHERE [type_desc] = 'SQL_STORED_PROCEDURE' AND [name] = 'ClearData';");
            if (query < 1)
            {
                Database.ExecuteSqlCommand(
                    @"
                    CREATE PROCEDURE [dbo].[ClearData]
                    AS
                    BEGIN
                        DELETE FROM Payments;
                        DELETE FROM Bills;
                        DELETE FROM Settlements;
                    END");
            }
            Database.ExecuteSqlCommand("exec ClearData");
        }

        /// <inheritdoc />
        public DbSet<T> GetSet<T>()
            where T : class, IEntity => Set<T>();

        /// <inheritdoc />
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await base.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <inheritdoc />
        public new bool SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <inheritdoc />
        public void Add(IEntity entity) => base.Add(entity);

        /// <inheritdoc />
        public void AddRange(IEnumerable<IEntity> entities) => base.AddRange(entities);

        /// <inheritdoc />
        public void Remove(IEntity entity) => base.Remove(entity);

        /// <inheritdoc />
        public void RemoveRange(ICollection<IEntity> entities) => base.RemoveRange(entities);

        #endregion
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Qualco;Integrated Security=True");
            }
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            #region EzPay Model
            modelBuilder.Entity<Citizen>(
                entity =>
                    {
                        entity.ToTable("Citizens");
                        entity.Property(e => e.Id)
                            .HasColumnName("ID")
                            // .HasMaxLength(10)
                            .ValueGeneratedNever();
                        entity.Property(e => e.FirstName)
                            .HasMaxLength(30);
                        entity.Property(e => e.LastName)
                            .HasMaxLength(30);
                        entity.Property(e => e.PasswordHash)
                            .HasMaxLength(84);
                        entity.Property(e => e.Email)
                            .HasMaxLength(40)
                            .IsUnicode(false);
                        entity.Property(e => e.Address)
                            .HasMaxLength(30)
                            .IsUnicode(false);
                        entity.Property(e => e.County)
                            .HasMaxLength(30)
                            .IsUnicode(false);
                        entity.Property(e => e.PhoneNumber)
                            .HasMaxLength(13)
                            .IsUnicode(false);
                });

            modelBuilder.Entity<Bill>(
                entity =>
                    {
                        entity.ToTable("Bills");
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Id)
                            .HasColumnName("ID")
                            .ValueGeneratedNever();
                        entity.Property(e => e.CitizenId)
                            .HasColumnName("Citizen");
                        entity.Property(e => e.DueDate)
                            .HasColumnType("date");
                        entity.Property(e => e.Description)
                            .HasMaxLength(100)
                            .IsUnicode(false);
                        entity.Property(e => e.Amount)
                            .HasColumnType("decimal(8, 2)");

                        entity
                            .HasOne(c => c.Citizen)
                            .WithMany(b => b.Bills)
                            .HasForeignKey(c => c.CitizenId);
                        entity
                            .HasOne(b => b.Settlement)
                            .WithMany(s => s.Bills)
                            .HasForeignKey(b => b.SettlementId);
                });

            modelBuilder.Entity<Payment>(
                entity =>
                    {
                        entity.ToTable("Payments");
                        entity
                            .HasKey(e => e.BillId);
                        entity.Property(e => e.BillId)
                            .HasColumnName("Bill")
                            .ValueGeneratedNever();
                        entity.Property(e => e.Method)
                            .HasMaxLength(30)
                            .IsUnicode(false);
                        entity.Property(e => e.Date)
                            .HasColumnType("datetime");

                        entity
                            .HasOne(p => p.Bill)
                            .WithOne(b => b.Payment)
                            .HasForeignKey<Payment>(p => p.BillId)
                            .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<Settlement>(
                entity =>
                    {
                        entity.ToTable("Settlements");
                        entity.Property(e => e.Id)
                            .HasColumnName("ID")
                            .HasDefaultValueSql("(newsequentialid())");
                        entity.Property(e => e.TypeId)
                            .HasColumnName("Type");
                        entity.Property(e => e.Installments)
                            .HasMaxLength(3);

                        entity
                            .HasOne(s => s.Type)
                            .WithMany(st => st.Settlements)
                            .HasForeignKey(s => s.TypeId);
                        entity
                            .HasOne(s => s.Citizen)
                            .WithMany(c => c.Settlements)
                            .HasForeignKey(s => s.CitizenId)
                            .OnDelete(DeleteBehavior.Restrict);
                        entity
                            .HasMany(s => s.Bills)
                            .WithOne(b => b.Settlement)
                            .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<SettlementType>(
                entity =>
                    {
                        entity.ToTable("SettlementTypes");
;                        entity.Property(e => e.Id)
                            .HasColumnName("ID")
                            .HasDefaultValueSql("(newsequentialid())");
                        entity.Property(e => e.Downpayment)
                            .HasColumnType("decimal(4, 2)");
                        entity.Property(e => e.Interest)
                            .HasColumnType("decimal(4, 2)");
                        entity.Property(e => e.MaxInstallments)
                            .HasMaxLength(3);
                });
            #endregion

            #region Identity

            modelBuilder.Entity<CitizenClaim>(
                entity =>
                    {
                        entity.ToTable("_CitizenClaim");
                        entity.HasKey(e => e.CitizenClaimId);
                    });

            modelBuilder.Entity<CitizenLogin>(
                entity =>
                    {
                        entity.ToTable("_CitizenLogin");
                        entity.HasKey(e => e.Id);
                    });

            modelBuilder.Entity<CitizenRole>(
                entity =>
                    {
                        entity.ToTable("_CitizenRole");
                        entity.HasKey(e => e.Id);
                    });

            modelBuilder.Entity<CitizenToken>(
                entity =>
                    {
                        entity.ToTable("_CitizenToken");
                        entity.HasKey(e => e.Id);
                    });

            modelBuilder.Entity<Role>(
                entity =>
                    {
                        entity.ToTable("_Role");
                        entity.HasKey(e => e.RoleId);
                    });

            modelBuilder.Entity<RoleClaim>(
                entity =>
                    {
                        entity.ToTable("_RoleClaim");
                        entity.HasKey(e => e.RoleClaimId);
                    });
            #endregion
        }
    }
}
