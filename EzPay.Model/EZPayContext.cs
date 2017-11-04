namespace EzPay.Model
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.Model.Entities;
    using EzPay.Model.IdentityEntities;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "virtual members required for Lazy Loading")]
    public class EzPayContext : IdentityDbContext<Citizen, Role, long, CitizenClaim, CitizenRole,CitizenLogin, RoleClaim, CitizenToken >
    {
        /// <summary>
        /// Collection of registered Citizens
        /// </summary>
        public virtual DbSet<Citizen> Citizens { get; set; }

        /// <summary>
        /// Collection of bills
        /// </summary>
        public virtual DbSet<Bill> Bills { get; set; }

        /// <summary>
        /// Collection of payments
        /// </summary>
        public virtual DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Collection of Settlements
        /// </summary>
        public virtual DbSet<Settlement> Settlements { get; set; }

        /// <summary>
        /// Collection of SettlementTypes
        /// </summary>
        public virtual DbSet<SettlementType> SettlementTypes { get; set; }

        /// <summary>
        /// Collection of CitizenRoles
        /// </summary>
        public virtual DbSet<CitizenClaim> CitizenClaims { get; set; }

        /// <summary>
        /// Collection of CitizenLogins
        /// </summary>
        public virtual DbSet<CitizenLogin> CitizenLogins { get; set; }

        /// <summary>
        /// Collection of CitizenRoles
        /// </summary>
        public virtual DbSet<CitizenRole> CitizenRoles { get; set; }

        /// <summary>
        /// Collection of CitizenRoles
        /// </summary>
        public virtual DbSet<CitizenToken> CitizenTokens { get; set; }

        /// <summary>
        /// Collection of CitizenRoles
        /// </summary>
        public virtual DbSet<Role> EZRoles { get; set; }

        /// <summary>
        /// Collection of CitizenRoles
        /// </summary>
        public virtual DbSet<RoleClaim> EZRoleClaims { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Data Source=CGWORKSTATION\CGWORKSTATION;Initial Catalog=Qualco;Integrated Security=True");
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Qualco;Integrated Security=True");
            }
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Citizen>(
                entity =>
                {
                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(10)
                        .ValueGeneratedNever();

                    entity.Property(e => e.FirstName)
                        .HasMaxLength(30);
                    entity.Property(e => e.LastName)
                        .HasMaxLength(30);
                   // entity.Property(e => e.PasswordHash)
                   //   .HasMaxLength(64);//no max length
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
                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    entity.Property(e => e.Downpayment)
                        .HasColumnType("decimal(4, 2)");

                    entity.Property(e => e.Interest)
                        .HasColumnType("decimal(4, 2)");

                    entity.Property(e => e.MaxInstallments)
                        .HasMaxLength(3);
                });

            modelBuilder.Entity<CitizenClaim>(
                entity =>
                    {
                        entity.HasKey(e => e.CitizenClaimId);
                        
                    });

            modelBuilder.Entity<CitizenLogin>(
                entity =>
                    {
                        entity.HasKey(e => e.Id);

                    });

            modelBuilder.Entity<CitizenRole>(
                entity =>
                    {
                        entity.HasKey(e => e.Id);

                    });

            modelBuilder.Entity<CitizenToken>(
                entity =>
                    {
                        entity.HasKey(e => e.Id);

                    });

            modelBuilder.Entity<Role>(
                entity =>
                    {
                        entity.HasKey(e => e.RoleId);

                    });

            modelBuilder.Entity<RoleClaim>(
                entity =>
                    {
                        entity.HasKey(e => e.RoleClaimId);

                    });
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public EzPayContext()
        {
            Database.ExecuteSqlCommand(
                @"
            DROP PROCEDURE IF EXISTS dbo.ClearData");
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
    }
}
