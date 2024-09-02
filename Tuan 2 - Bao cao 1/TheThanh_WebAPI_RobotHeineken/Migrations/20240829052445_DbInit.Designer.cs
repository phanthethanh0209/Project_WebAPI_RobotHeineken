﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheThanh_WebAPI_RobotHeineken.Data;

#nullable disable

namespace TheThanh_WebAPI_RobotHeineken.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20240829052445_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationID"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.HasIndex("LocationName")
                        .IsUnique();

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.RecyclingMachine", b =>
                {
                    b.Property<int>("MachineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachineID"), 1L, 1);

                    b.Property<int>("ContainerStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeinekenCount")
                        .HasColumnType("int");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<int>("MachineCode")
                        .HasColumnType("int");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OtherCount")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("TotalInteractions")
                        .HasColumnType("int");

                    b.HasKey("MachineID");

                    b.HasIndex("LocationID");

                    b.HasIndex("MachineCode")
                        .IsUnique();

                    b.ToTable("RecyclingMachine", (string)null);
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.Robot", b =>
                {
                    b.Property<int>("RobotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RobotID"), 1L, 1);

                    b.Property<int>("BatteryLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(100);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastAccess")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocationID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("RobotName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("RobotID");

                    b.HasIndex("LocationID");

                    b.HasIndex("TypeID");

                    b.ToTable("Robot", (string)null);
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.RobotType", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeID");

                    b.ToTable("RobotType", (string)null);
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.RecyclingMachine", b =>
                {
                    b.HasOne("TheThanh_WebAPI_RobotHeineken.Data.Location", "Location")
                        .WithMany("RecyclingMachines")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.Robot", b =>
                {
                    b.HasOne("TheThanh_WebAPI_RobotHeineken.Data.Location", "Location")
                        .WithMany("Robots")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TheThanh_WebAPI_RobotHeineken.Data.RobotType", "Type")
                        .WithMany("Robots")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.Location", b =>
                {
                    b.Navigation("RecyclingMachines");

                    b.Navigation("Robots");
                });

            modelBuilder.Entity("TheThanh_WebAPI_RobotHeineken.Data.RobotType", b =>
                {
                    b.Navigation("Robots");
                });
#pragma warning restore 612, 618
        }
    }
}
