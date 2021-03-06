﻿// <auto-generated />
using EzPay.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EzPay.Context.SqlServer.Migrations
{
    [DbContext(typeof(EzPaySqlServerContext))]
    [Migration("20171118100015_Nullable Settlement FK in Bills")]
    partial class NullableSettlementFKinBills
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EzPay.Model.Entities.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<long>("CitizenId")
                        .HasColumnName("Citizen");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("SettlementId");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("SettlementId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("EzPay.Model.Entities.Citizen", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("County")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(84);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(13)
                        .IsUnicode(false);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Citizens");
                });

            modelBuilder.Entity("EzPay.Model.Entities.Payment", b =>
                {
                    b.Property<Guid>("BillId")
                        .HasColumnName("Bill");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Method")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("BillId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("EzPay.Model.Entities.Settlement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<long>("CitizenId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Installments")
                        .HasMaxLength(3);

                    b.Property<Guid>("TypeId")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("TypeId");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("EzPay.Model.Entities.SettlementType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<decimal>("Downpayment")
                        .HasColumnType("decimal(4, 2)");

                    b.Property<decimal>("Interest")
                        .HasColumnType("decimal(4, 2)");

                    b.Property<int>("MaxInstallments")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("SettlementTypes");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.CitizenClaim", b =>
                {
                    b.Property<long>("CitizenClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("Id");

                    b.Property<long>("UserId");

                    b.HasKey("CitizenClaimId");

                    b.ToTable("_CitizenClaim");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.CitizenLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("_CitizenLogin");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.CitizenRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("_CitizenRole");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.CitizenToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<long>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("_CitizenToken");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<long>("Id");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("RoleId");

                    b.ToTable("_Role");
                });

            modelBuilder.Entity("EzPay.Model.IdentityEntities.RoleClaim", b =>
                {
                    b.Property<long>("RoleClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("Id");

                    b.Property<long>("RoleId");

                    b.HasKey("RoleClaimId");

                    b.ToTable("_RoleClaim");
                });

            modelBuilder.Entity("EzPay.Model.Entities.Bill", b =>
                {
                    b.HasOne("EzPay.Model.Entities.Citizen", "Citizen")
                        .WithMany("Bills")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EzPay.Model.Entities.Settlement", "Settlement")
                        .WithMany("Bills")
                        .HasForeignKey("SettlementId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("EzPay.Model.Entities.Payment", b =>
                {
                    b.HasOne("EzPay.Model.Entities.Bill", "Bill")
                        .WithOne("Payment")
                        .HasForeignKey("EzPay.Model.Entities.Payment", "BillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EzPay.Model.Entities.Settlement", b =>
                {
                    b.HasOne("EzPay.Model.Entities.Citizen", "Citizen")
                        .WithMany("Settlements")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EzPay.Model.Entities.SettlementType", "Type")
                        .WithMany("Settlements")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
