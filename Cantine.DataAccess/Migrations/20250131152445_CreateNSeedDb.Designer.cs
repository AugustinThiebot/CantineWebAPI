﻿// <auto-generated />
using System;
using Cantine.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cantine.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250131152445_CreateNSeedDb")]
    partial class CreateNSeedDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.12");

            modelBuilder.Entity("Cantine.Domain.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Budget")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3720da0a-9109-48c5-89aa-acc7b7684294"),
                            Budget = 100m,
                            CategoryId = new Guid("1efe7a31-8dcc-4ff0-9b2d-5f148e2989cc"),
                            Name = "Michel Blanc"
                        });
                });

            modelBuilder.Entity("Cantine.Domain.ClientCategory", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DiscountType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DiscountValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("ClientCategories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("3c2d2d86-441c-4a6b-abc4-08edc0149ab1"),
                            DiscountType = "Fixed",
                            DiscountValue = 7.5m,
                            Name = "Interne"
                        },
                        new
                        {
                            CategoryId = new Guid("ee613ca8-47ae-49dc-931e-acecf4c1930e"),
                            DiscountType = "Fixed",
                            DiscountValue = 6m,
                            Name = "Prestataire"
                        },
                        new
                        {
                            CategoryId = new Guid("ad100c63-43fc-45d9-944f-31d70d6befa8"),
                            DiscountType = "Percentage",
                            DiscountValue = 100m,
                            Name = "VIP"
                        },
                        new
                        {
                            CategoryId = new Guid("1efe7a31-8dcc-4ff0-9b2d-5f148e2989cc"),
                            DiscountType = "Fixed",
                            DiscountValue = 10m,
                            Name = "Stagiaire"
                        },
                        new
                        {
                            CategoryId = new Guid("b941481f-d14c-4a94-bb5e-2b29489ec86a"),
                            DiscountType = "None",
                            DiscountValue = 0m,
                            Name = "Visiteur"
                        });
                });

            modelBuilder.Entity("Cantine.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c5cc549e-30c2-452d-a9d5-133fc7641c16"),
                            Price = 1m,
                            ProductName = "Boisson"
                        },
                        new
                        {
                            Id = new Guid("61c5959d-6089-4291-8b5a-467340dfd859"),
                            Price = 1m,
                            ProductName = "Fromage"
                        },
                        new
                        {
                            Id = new Guid("6d024167-3abd-45d0-8326-abcbe6750700"),
                            Price = 0.40m,
                            ProductName = "Pain"
                        },
                        new
                        {
                            Id = new Guid("8e144565-6da3-4ffe-93ba-6a8f72f77fe8"),
                            Price = 4m,
                            ProductName = "Petit Salade Bar"
                        },
                        new
                        {
                            Id = new Guid("a56a4686-5ab2-46c4-b8dd-b24afbd0f123"),
                            Price = 6m,
                            ProductName = "Grand Salade Bar"
                        },
                        new
                        {
                            Id = new Guid("a24f0638-dd84-4cea-87d2-42c63dfc767a"),
                            Price = 1m,
                            ProductName = "Portion de fruit"
                        },
                        new
                        {
                            Id = new Guid("fa025408-f032-4013-b855-5653cedab972"),
                            Price = 3m,
                            ProductName = "Entrée"
                        },
                        new
                        {
                            Id = new Guid("62f8e186-3853-4251-813f-a5c5cede5af8"),
                            Price = 6m,
                            ProductName = "Plat"
                        },
                        new
                        {
                            Id = new Guid("ca8a3f05-7322-4b3a-844c-6dbcd2e7af3c"),
                            Price = 3m,
                            ProductName = "Dessert"
                        });
                });

            modelBuilder.Entity("Cantine.Domain.Client", b =>
                {
                    b.HasOne("Cantine.Domain.ClientCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
