﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rentering.Infra;

namespace Rentering.Infra.Migrations
{
    [DbContext(typeof(RenteringDbContext))]
    partial class RenteringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rentering.Accounts.Domain.Entities.AccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.AccountContractsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantRole")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ContractId");

                    b.ToTable("AccountContracts");
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.ContractEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("date");

                    b.Property<string>("ContractName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("date");

                    b.Property<int>("ContractState")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentDueDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.ContractPaymentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Month")
                        .HasColumnType("Date");

                    b.Property<int>("PayerPaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverPaymentStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("ContractPayments");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.CorporationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Corporation");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.MonthlyBalanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CorporationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Month")
                        .HasColumnType("Date");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalProfit")
                        .HasColumnType("decimal(19,5)");

                    b.HasKey("Id");

                    b.HasIndex("CorporationId");

                    b.ToTable("Corporation_MonthlyBalance");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.ParticipantBalanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(19,5)");

                    b.Property<int>("MonthlyBalanceId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyBalanceId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Corporation_ParticipantBalance");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.ParticipantEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("CorporationId")
                        .HasColumnType("int");

                    b.Property<int>("InvitationStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("SharedPercentage")
                        .HasColumnType("decimal(19,5)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CorporationId");

                    b.ToTable("Corporation_Participants");
                });

            modelBuilder.Entity("Rentering.Accounts.Domain.Entities.AccountEntity", b =>
                {
                    b.OwnsOne("Rentering.Accounts.Domain.ValueObjects.EmailValueObject", "Email", b1 =>
                        {
                            b1.Property<int>("AccountEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Email");

                            b1.HasKey("AccountEntityId");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountEntityId");
                        });

                    b.OwnsOne("Rentering.Accounts.Domain.ValueObjects.PasswordValueObject", "Password", b1 =>
                        {
                            b1.Property<int>("AccountEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Password")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Password");

                            b1.HasKey("AccountEntityId");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountEntityId");
                        });

                    b.OwnsOne("Rentering.Accounts.Domain.ValueObjects.PersonNameValueObject", "Name", b1 =>
                        {
                            b1.Property<int>("AccountEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("AccountEntityId");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountEntityId");
                        });

                    b.OwnsOne("Rentering.Accounts.Domain.ValueObjects.UsernameValueObject", "Username", b1 =>
                        {
                            b1.Property<int>("AccountEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Username")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Username");

                            b1.HasKey("AccountEntityId");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountEntityId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Username")
                        .IsRequired();
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.AccountContractsEntity", b =>
                {
                    b.HasOne("Rentering.Accounts.Domain.Entities.AccountEntity", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentering.Contracts.Domain.Entities.ContractEntity", "Contract")
                        .WithMany("Participants")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.ContractEntity", b =>
                {
                    b.OwnsOne("Rentering.Contracts.Domain.ValueObjects.PriceValueObject", "RentPrice", b1 =>
                        {
                            b1.Property<int>("ContractEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Price")
                                .HasColumnType("decimal(19,5)")
                                .HasColumnName("Price");

                            b1.HasKey("ContractEntityId");

                            b1.ToTable("Contracts");

                            b1.WithOwner()
                                .HasForeignKey("ContractEntityId");
                        });

                    b.Navigation("RentPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.ContractPaymentEntity", b =>
                {
                    b.HasOne("Rentering.Contracts.Domain.Entities.ContractEntity", null)
                        .WithMany("Payments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Rentering.Contracts.Domain.ValueObjects.PriceValueObject", "RentPrice", b1 =>
                        {
                            b1.Property<int>("ContractPaymentEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Price")
                                .HasColumnType("decimal(19,5)")
                                .HasColumnName("Price");

                            b1.HasKey("ContractPaymentEntityId");

                            b1.ToTable("ContractPayments");

                            b1.WithOwner()
                                .HasForeignKey("ContractPaymentEntityId");
                        });

                    b.Navigation("RentPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.MonthlyBalanceEntity", b =>
                {
                    b.HasOne("Rentering.Corporation.Domain.Entities.CorporationEntity", null)
                        .WithMany("MonthlyBalances")
                        .HasForeignKey("CorporationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.ParticipantBalanceEntity", b =>
                {
                    b.HasOne("Rentering.Corporation.Domain.Entities.MonthlyBalanceEntity", "MonthlyBalance")
                        .WithMany("ParticipantBalances")
                        .HasForeignKey("MonthlyBalanceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Rentering.Corporation.Domain.Entities.ParticipantEntity", "Participant")
                        .WithMany("ParticipantBalances")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MonthlyBalance");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.ParticipantEntity", b =>
                {
                    b.HasOne("Rentering.Accounts.Domain.Entities.AccountEntity", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentering.Corporation.Domain.Entities.CorporationEntity", "Corporation")
                        .WithMany("Participants")
                        .HasForeignKey("CorporationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Corporation");
                });

            modelBuilder.Entity("Rentering.Contracts.Domain.Entities.ContractEntity", b =>
                {
                    b.Navigation("Participants");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.CorporationEntity", b =>
                {
                    b.Navigation("MonthlyBalances");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.MonthlyBalanceEntity", b =>
                {
                    b.Navigation("ParticipantBalances");
                });

            modelBuilder.Entity("Rentering.Corporation.Domain.Entities.ParticipantEntity", b =>
                {
                    b.Navigation("ParticipantBalances");
                });
#pragma warning restore 612, 618
        }
    }
}
