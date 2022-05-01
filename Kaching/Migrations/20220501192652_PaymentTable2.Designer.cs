﻿// <auto-generated />
using System;
using Kaching.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kaching.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220501192652_PaymentTable2")]
    partial class PaymentTable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Kaching.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"), 1L, 1);

                    b.Property<int?>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ExpenseId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Expense");
                });

            modelBuilder.Entity("Kaching.Models.ExpenseEvent", b =>
                {
                    b.Property<int>("ExpenseEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseEventId"), 1L, 1);

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ExpenseEventId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseEvent");
                });

            modelBuilder.Entity("Kaching.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentPeriod")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Kaching.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<string>("ConnectedUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConnectedUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Kaching.Models.Expense", b =>
                {
                    b.HasOne("Kaching.Models.Person", "Creator")
                        .WithMany("ExpensesCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Kaching.Models.ExpenseEvent", b =>
                {
                    b.HasOne("Kaching.Models.Person", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaching.Models.Expense", "Expense")
                        .WithMany("ExpenseEvents")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("Kaching.Models.Payment", b =>
                {
                    b.HasOne("Kaching.Models.Person", "Receiver")
                        .WithMany("PaymentsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Kaching.Models.Person", "Sender")
                        .WithMany("PaymentsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Kaching.Models.Expense", b =>
                {
                    b.Navigation("ExpenseEvents");
                });

            modelBuilder.Entity("Kaching.Models.Person", b =>
                {
                    b.Navigation("ExpensesCreated");

                    b.Navigation("PaymentsReceived");

                    b.Navigation("PaymentsSent");
                });
#pragma warning restore 612, 618
        }
    }
}
