﻿// <auto-generated />
using EzPay.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EzPay.Model.Migrations
{
    [DbContext(typeof(EzPayContext))]
    [Migration("20171017113200_Minor reformation")]
    partial class Minorreformation
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
                        .HasColumnName("ID")
                        .HasMaxLength(10);

                    b.Property<string>("Address")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("County")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(64);

                    b.Property<string>("Telephone")
                        .HasMaxLength(13)
                        .IsUnicode(false);

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

            modelBuilder.Entity("EzPay.Model.Entities.Bill", b =>
                {
                    b.HasOne("EzPay.Model.Entities.Citizen", "Citizen")
                        .WithMany("Bills")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EzPay.Model.Entities.Settlement", "Settlement")
                        .WithMany("Bills")
                        .HasForeignKey("SettlementId")
                        .OnDelete(DeleteBehavior.Cascade);
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
