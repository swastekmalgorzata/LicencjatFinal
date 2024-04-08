﻿// <auto-generated />
using System;
using ApartmentManagmentBlazorAppCopy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApartmentManagmentBlazorAppCopy.Migrations
{
    [DbContext(typeof(ApartmentsContext))]
    [Migration("20240325182113_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Apartment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.Chore", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("ChoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("NameOfDoer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chores");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.ExploitationCost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("CustomBreakdown")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("DeadLine")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("EvenBreakDown")
                        .HasColumnType("float");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WholeAmount")
                        .HasColumnType("float");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExploitationCosts");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("DocByte")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.Meter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<double>("EnrgyReading")
                        .HasColumnType("float");

                    b.Property<double>("GasReading")
                        .HasColumnType("float");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WaterReading")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.PurchaseRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<double?>("Cost")
                        .HasColumnType("float");

                    b.Property<bool>("IsBought")
                        .HasColumnType("bit");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PurchaseRecords");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.Rent", b =>
                {
                    b.Property<long>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RentId"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("CustomBreakdown")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("DeadLine")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("EvenBreakDown")
                        .HasColumnType("float");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WholeAmount")
                        .HasColumnType("float");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.Roommate", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoommateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RoommateId"));

                    b.HasKey("UserId");

                    b.ToTable("Roommates");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.ExploitationCost", b =>
                {
                    b.OwnsMany("ApartmentManagmentBlazorAppCopy.Models.RoommatePart", "CustomCostBreakDown", b1 =>
                        {
                            b1.Property<long>("ExploitationCostId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("IsPaid")
                                .HasColumnType("bit");

                            b1.Property<string>("RoommateName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Share")
                                .HasColumnType("float");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ExploitationCostId", "Id");

                            b1.ToTable("ExploitationCosts_CustomCostBreakDown");

                            b1.WithOwner()
                                .HasForeignKey("ExploitationCostId");
                        });

                    b.Navigation("CustomCostBreakDown");
                });

            modelBuilder.Entity("ApartmentManagmentBlazorAppCopy.Models.Rent", b =>
                {
                    b.OwnsMany("ApartmentManagmentBlazorAppCopy.Models.RoommatePart", "CustomCostBreakDown", b1 =>
                        {
                            b1.Property<long>("RentId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("IsPaid")
                                .HasColumnType("bit");

                            b1.Property<string>("RoommateName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Share")
                                .HasColumnType("float");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RentId", "Id");

                            b1.ToTable("Rents_CustomCostBreakDown");

                            b1.WithOwner()
                                .HasForeignKey("RentId");
                        });

                    b.Navigation("CustomCostBreakDown");
                });
#pragma warning restore 612, 618
        }
    }
}
