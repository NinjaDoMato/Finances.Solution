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
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.ToTable("Entries");
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

                    b.ToTable("Reserves");
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

                    b.ToTable("ReserveInvestmentMaps");
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

            modelBuilder.Entity("Finances.Database.Entities.Investment", b =>
                {
                    b.Navigation("SourceReserves");
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
