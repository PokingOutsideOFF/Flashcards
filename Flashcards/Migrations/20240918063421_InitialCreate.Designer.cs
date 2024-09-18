﻿// <auto-generated />
using System;
using Flashcards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flashcards.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240918063421_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Flashcards.Models.Flashcard", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"), 1L, 1);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StackCardId")
                        .HasColumnType("int");

                    b.Property<int>("StackId")
                        .HasColumnType("int");

                    b.HasKey("CardId");

                    b.HasIndex("StackId");

                    b.ToTable("Flashcard");
                });

            modelBuilder.Entity("Flashcards.Models.Stack", b =>
                {
                    b.Property<int>("StackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StackId"), 1L, 1);

                    b.Property<string>("StackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StackId");

                    b.ToTable("Stack");
                });

            modelBuilder.Entity("Flashcards.Models.StudySession", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("StackId")
                        .HasColumnType("int");

                    b.Property<string>("StackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SessionId");

                    b.HasIndex("StackId");

                    b.ToTable("StudySession");
                });

            modelBuilder.Entity("Flashcards.Models.Flashcard", b =>
                {
                    b.HasOne("Flashcards.Models.Stack", "Stack")
                        .WithMany("Flashcards")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("Flashcards.Models.StudySession", b =>
                {
                    b.HasOne("Flashcards.Models.Stack", "Stack")
                        .WithMany("StudySessions")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("Flashcards.Models.Stack", b =>
                {
                    b.Navigation("Flashcards");

                    b.Navigation("StudySessions");
                });
#pragma warning restore 612, 618
        }
    }
}
