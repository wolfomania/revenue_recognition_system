﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RevenueRecognitionSystem.Data;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KRS")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PESEL")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ClientId");

                    b.HasIndex("KRS")
                        .IsUnique()
                        .HasFilter("[KRS] IS NOT NULL");

                    b.HasIndex("PESEL")
                        .IsUnique()
                        .HasFilter("[PESEL] IS NOT NULL");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Contract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<int>("AdditionalSupportYears")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContractId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfferType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            DiscountId = 1,
                            Name = "Previous Customer",
                            OfferType = " Discount for previous customers 5%",
                            Value = 5.0
                        });
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            Login = "admin",
                            Password = "42g8uo1nnaYms3kh+SmRXPW7moKobJvkiIvwaAlVKlA=",
                            RefreshToken = "da24qiCKeWjeYpWlo2XhS+Zaxj27UjBJbHKt4EJWwxk=",
                            RefreshTokenExp = new DateTime(2024, 6, 29, 15, 36, 36, 293, DateTimeKind.Local).AddTicks(1488),
                            Role = "Admin",
                            Salt = "CPUvxMPA/wRz0O1ufvQRVw=="
                        });
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PaymentId");

                    b.HasIndex("ContractId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CurrentVersion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("SoftwareId");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.SoftwareDiscount", b =>
                {
                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.HasKey("SoftwareId", "DiscountId");

                    b.HasIndex("DiscountId");

                    b.ToTable("SoftwareDiscounts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("PricePerPeriod")
                        .HasColumnType("float");

                    b.Property<string>("RenewalPeriod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Contract", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Software", "Software")
                        .WithMany("Contracts")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Payment", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Contract", "Contract")
                        .WithMany("Payments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.SoftwareDiscount", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Discount", "Discount")
                        .WithMany("SoftwareDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Software", "Software")
                        .WithMany("SoftwareDiscounts")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Subscription", b =>
                {
                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Client", "Client")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevenueRecognitionSystem.Models.Domain.Software", "Software")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Client", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Contract", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Discount", b =>
                {
                    b.Navigation("SoftwareDiscounts");
                });

            modelBuilder.Entity("RevenueRecognitionSystem.Models.Domain.Software", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("SoftwareDiscounts");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}