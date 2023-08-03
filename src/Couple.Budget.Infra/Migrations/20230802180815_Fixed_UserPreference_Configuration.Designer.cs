﻿// <auto-generated />
using System;
using Couple.Budget.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Couple.Budget.Infra.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230802180815_Fixed_UserPreference_Configuration")]
    partial class Fixed_UserPreference_Configuration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.BudgetDay", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.ToTable("BudgetDays", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.BudgetDayExpense", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BudgetDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetDayId");

                    b.ToTable("BudgetDayExpenses", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.Suggest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.ToTable("Suggests", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.UserPreference", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPreferences", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.UserPreferenceDayOfWeek", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<Guid>("UserPreferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserPreferenceId");

                    b.ToTable("UserPreferenceDayOfWeeks", (string)null);
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.Budget", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Users.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Couple.Budget.Domain.Budgets.ValueObjects.Month", "Month", b1 =>
                        {
                            b1.Property<Guid>("BudgetId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("MonthName")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.Property<int>("MonthNumber")
                                .HasColumnType("int");

                            b1.HasKey("BudgetId");

                            b1.ToTable("Budgets");

                            b1.WithOwner()
                                .HasForeignKey("BudgetId");
                        });

                    b.Navigation("Month")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.BudgetDay", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Budgets.Entities.Budget", "Budget")
                        .WithMany("BudgetDays")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.BudgetDayExpense", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Budgets.Entities.BudgetDay", "BudgetDay")
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetDay");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.Suggest", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Budgets.Entities.Budget", "Budget")
                        .WithMany("Suggests")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.User", b =>
                {
                    b.OwnsOne("Couple.Budget.Core.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.UserPreference", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Users.Entities.User", "User")
                        .WithOne("UserPreference")
                        .HasForeignKey("Couple.Budget.Domain.Users.Entities.UserPreference", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.UserPreferenceDayOfWeek", b =>
                {
                    b.HasOne("Couple.Budget.Domain.Users.Entities.UserPreference", "UserPreference")
                        .WithMany("UserPreferenceDaysOfWeeks")
                        .HasForeignKey("UserPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserPreference");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.Budget", b =>
                {
                    b.Navigation("BudgetDays");

                    b.Navigation("Suggests");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Budgets.Entities.BudgetDay", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.User", b =>
                {
                    b.Navigation("UserPreference")
                        .IsRequired();
                });

            modelBuilder.Entity("Couple.Budget.Domain.Users.Entities.UserPreference", b =>
                {
                    b.Navigation("UserPreferenceDaysOfWeeks");
                });
#pragma warning restore 612, 618
        }
    }
}
