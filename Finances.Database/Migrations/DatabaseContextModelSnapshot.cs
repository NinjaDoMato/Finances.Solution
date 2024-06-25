﻿// <auto-generated />
using System;
using Finances.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Finances.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Finances.Database.Entities.Cost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("CassiaPercentage")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DanielPercentage")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("Finances.Database.Entities.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ReserveId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ReserveId");

                    b.ToTable("Entries", (string)null);
                });

            modelBuilder.Entity("Finances.Database.Entities.Installment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InstallmentNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Paid")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.ToTable("PurchaseInstallments");
                });

            modelBuilder.Entity("Finances.Database.Entities.Investment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Account")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Rentability")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("StartAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("Finances.Database.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CostId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DatePaid")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CostId");

                    b.ToTable("CostPayments");
                });

            modelBuilder.Entity("Finances.Database.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Owner")
                        .HasColumnType("int");

                    b.Property<string>("ProductUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Finances.Database.Entities.Reserve", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Goal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Owner")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reserves", (string)null);
                });

            modelBuilder.Entity("Finances.Database.Entities.ReserveInvestment", b =>
                {
                    b.Property<Guid>("ReserveId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("InvestmentId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ReserveId", "InvestmentId");

                    b.HasIndex("InvestmentId");

                    b.ToTable("ReserveInvestmentsMaps", (string)null);
                });

            modelBuilder.Entity("Finances.Database.Entities.Entry", b =>
                {
                    b.HasOne("Finances.Database.Entities.Reserve", "Reserve")
                        .WithMany("Entries")
                        .HasForeignKey("ReserveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserve");
                });

            modelBuilder.Entity("Finances.Database.Entities.Installment", b =>
                {
                    b.HasOne("Finances.Database.Entities.Purchase", "Purchase")
                        .WithMany("Installments")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Finances.Database.Entities.Payment", b =>
                {
                    b.HasOne("Finances.Database.Entities.Cost", "Cost")
                        .WithMany("Payments")
                        .HasForeignKey("CostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cost");
                });

            modelBuilder.Entity("Finances.Database.Entities.ReserveInvestment", b =>
                {
                    b.HasOne("Finances.Database.Entities.Investment", "Investment")
                        .WithMany("SourceReserves")
                        .HasForeignKey("InvestmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Finances.Database.Entities.Reserve", "Reserve")
                        .WithMany("LinkedInvestments")
                        .HasForeignKey("ReserveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Investment");

                    b.Navigation("Reserve");
                });

            modelBuilder.Entity("Finances.Database.Entities.Cost", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Finances.Database.Entities.Investment", b =>
                {
                    b.Navigation("SourceReserves");
                });

            modelBuilder.Entity("Finances.Database.Entities.Purchase", b =>
                {
                    b.Navigation("Installments");
                });

            modelBuilder.Entity("Finances.Database.Entities.Reserve", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("LinkedInvestments");
                });
#pragma warning restore 612, 618
        }
    }
}
