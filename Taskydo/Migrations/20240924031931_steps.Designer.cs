﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taskydo;

#nullable disable

namespace Taskydo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240924031931_steps")]
    partial class steps
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Taskydo.Entities.Step", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDone")
                        .HasColumnType("bit");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<int>("taskId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("taskId");

                    b.ToTable("steps");
                });

            modelBuilder.Entity("Taskydo.Entities.Task", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("id");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("Taskydo.Entities.Step", b =>
                {
                    b.HasOne("Taskydo.Entities.Task", "Task")
                        .WithMany("steps")
                        .HasForeignKey("taskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Taskydo.Entities.Task", b =>
                {
                    b.Navigation("steps");
                });
#pragma warning restore 612, 618
        }
    }
}
