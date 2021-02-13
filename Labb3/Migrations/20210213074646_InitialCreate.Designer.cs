﻿// <auto-generated />
using System;
using Labb3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Labb3.Migrations
{
    [DbContext(typeof(MusicContext))]
    [Migration("20210213074646_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Labb3.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Labb3.Models.Onloan", b =>
                {
                    b.Property<int>("OnloanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("TEXT");

                    b.Property<string>("FriendName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RecordId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OnloanId");

                    b.HasIndex("RecordId");

                    b.ToTable("Onloan");
                });

            modelBuilder.Entity("Labb3.Models.Record", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RecordName")
                        .HasColumnType("TEXT");

                    b.HasKey("RecordId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("Labb3.Models.Onloan", b =>
                {
                    b.HasOne("Labb3.Models.Record", "Record")
                        .WithMany("Onloans")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Record");
                });

            modelBuilder.Entity("Labb3.Models.Record", b =>
                {
                    b.HasOne("Labb3.Models.Artist", "Artist")
                        .WithMany("Records")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Labb3.Models.Artist", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("Labb3.Models.Record", b =>
                {
                    b.Navigation("Onloans");
                });
#pragma warning restore 612, 618
        }
    }
}
