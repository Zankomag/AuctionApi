﻿// <auto-generated />

using System;
using Auction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.Infrastructure.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20210329013708_RemoveSeller")]
    partial class RemoveSeller
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualCloseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AuctionItemCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("AuctionItemStatusCodeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime>("PlannedCloseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WinningBidId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuctionItemCategoryId");

                    b.HasIndex("AuctionItemStatusCodeId");

                    b.HasIndex("SellerId");

                    b.HasIndex("WinningBidId")
                        .IsUnique()
                        .HasFilter("[WinningBidId] IS NOT NULL");

                    b.ToTable("AuctionItems");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("AuctionItemCategories");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItemStatusCode", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("AuctionItemStatusCodes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Scheduled"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Started"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Finished"
                        });
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuctionItemId")
                        .HasColumnType("int");

                    b.Property<int>("BidderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionItemId");

                    b.HasIndex("BidderId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItem", b =>
                {
                    b.HasOne("AuctionAPI.Core.Entities.AuctionItemCategory", "AuctionItemCategory")
                        .WithMany()
                        .HasForeignKey("AuctionItemCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionAPI.Core.Entities.AuctionItemStatusCode", "AuctionItemStatusCode")
                        .WithMany("AuctionItems")
                        .HasForeignKey("AuctionItemStatusCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionAPI.Core.Entities.User", "Seller")
                        .WithMany("AuctionItems")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionAPI.Core.Entities.Bid", "WinningBid")
                        .WithOne("AuctionItem")
                        .HasForeignKey("AuctionAPI.Core.Entities.AuctionItem", "WinningBidId");

                    b.Navigation("AuctionItemCategory");

                    b.Navigation("AuctionItemStatusCode");

                    b.Navigation("Seller");

                    b.Navigation("WinningBid");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItemCategory", b =>
                {
                    b.HasOne("AuctionAPI.Core.Entities.AuctionItemCategory", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.Bid", b =>
                {
                    b.HasOne("AuctionAPI.Core.Entities.AuctionItem", null)
                        .WithMany("Bids")
                        .HasForeignKey("AuctionItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionAPI.Core.Entities.User", "Bidder")
                        .WithMany("Bids")
                        .HasForeignKey("BidderId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Bidder");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItem", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItemCategory", b =>
                {
                    b.Navigation("ChildCategories");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.AuctionItemStatusCode", b =>
                {
                    b.Navigation("AuctionItems");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.Bid", b =>
                {
                    b.Navigation("AuctionItem");
                });

            modelBuilder.Entity("AuctionAPI.Core.Entities.User", b =>
                {
                    b.Navigation("AuctionItems");

                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
