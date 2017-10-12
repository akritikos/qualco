namespace EzPay.Model
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.Model.Entities;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "virtual members required for Lazy Loading")]
    public class EzPayContext : DbContext
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
        /// Mapping of Bills with agreed Settlements
        /// </summary>
        public virtual DbSet<SettledBills> Settled { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=qualco2;Integrated Security=True");
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
                    entity.Property(e => e.Password)
                        .HasMaxLength(64);
                    entity.Property(e => e.Email)
                        .HasMaxLength(40)
                        .IsUnicode(false);
                    entity.Property(e => e.Address)
                        .HasMaxLength(150)
                        .IsUnicode(false);
                    entity.Property(e => e.County)
                        .HasMaxLength(15)
                        .IsUnicode(false);
                    entity.Property(e => e.Telephone)
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
                        .HasColumnType("decimal(7, 2)");

                    entity
                        .HasOne(c => c.Citizen)
                        .WithMany(b => b.Bills)
                        .HasForeignKey(c => c.CitizenId);
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

            modelBuilder.Entity<SettledBills>(
                entity =>
                {
                    entity
                        .HasKey(t => new { t.BillId, t.SettlementId });

                    entity.Property(e => e.BillId)
                        .HasColumnName("Bill");
                    entity.Property(e => e.SettlementId)
                        .HasColumnName("Settlement");
                });
        }
    }
}
