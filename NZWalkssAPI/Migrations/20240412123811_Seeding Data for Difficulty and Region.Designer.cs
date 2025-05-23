﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalkssAPI.Data;

#nullable disable

namespace NZWalkssAPI.Migrations
{
    [DbContext(typeof(NZWalkssDbContext))]
    [Migration("20240412123811_Seeding Data for Difficulty and Region")]
    partial class SeedingDataforDifficultyandRegion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalkssAPI.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0266074e-cc21-4a13-8d8a-24f0e85ccb6c"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("3f82955a-5375-422c-9e29-077ef19d53d3"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("283568c0-9ab1-47c6-a884-bb889b167750"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZWalkssAPI.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reagions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("272cfc74-77f0-49eb-aad6-b11283cdaa0d"),
                            Code = "SDT",
                            Name = "Sandton",
                            RegionImageURL = "https://th.bing.com/th/id/OIP.MI2hagCeMic8FNoH32HnCgAAAA?rs=1&pid=ImgDetMain"
                        },
                        new
                        {
                            Id = new Guid("fed490ce-1f12-4e1e-8aea-160ffcfea7e4"),
                            Code = "MRD",
                            Name = "Midrand",
                            RegionImageURL = "https://www.sa-venues.com/maps/atlas/gau_midrand.gif"
                        },
                        new
                        {
                            Id = new Guid("02c2fae2-2957-4fb5-8ddf-f67b90bc129d"),
                            Code = "RDP",
                            Name = "Roodeport",
                            RegionImageURL = "https://www.sa-venues.com/maps/atlas/gau_roodepoort.gif"
                        });
                });

            modelBuilder.Entity("NZWalkssAPI.Models.Domain.Walks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKM")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalkssAPI.Models.Domain.Walks", b =>
                {
                    b.HasOne("NZWalkssAPI.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalkssAPI.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
