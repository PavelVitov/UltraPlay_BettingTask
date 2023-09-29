﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UltraPlay.Data;

#nullable disable

namespace UltraPlay.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UltraPlay.Data.Models.Bet", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MatchId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Bet>", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<int>("UpdateType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EntityId");

                    b.ToTable("BetInformers");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Match>", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<int>("UpdateType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EntityId");

                    b.ToTable("MatchInformers");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Odd>", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<int>("UpdateType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EntityId");

                    b.ToTable("OddInformers");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SportID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Match", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("MatchType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("EventId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Odd", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialBetValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("BetId");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Sport", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Bet", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Match", null)
                        .WithMany("Bets")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Bet>", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Bet", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Match>", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Match", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.EntityUpdateInformer<UltraPlay.Data.Models.Odd>", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Odd", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Event", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Sport", null)
                        .WithMany("Events")
                        .HasForeignKey("SportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Match", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Event", null)
                        .WithMany("Matches")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Odd", b =>
                {
                    b.HasOne("UltraPlay.Data.Models.Bet", null)
                        .WithMany("Odds")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Bet", b =>
                {
                    b.Navigation("Odds");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Event", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Match", b =>
                {
                    b.Navigation("Bets");
                });

            modelBuilder.Entity("UltraPlay.Data.Models.Sport", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
