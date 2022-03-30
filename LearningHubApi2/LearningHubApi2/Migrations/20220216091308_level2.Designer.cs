﻿// <auto-generated />
using System;
using LearningHubApi2.Data_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LearningHubApi2.Migrations
{
    [DbContext(typeof(LearningHubApiDbContext))]
    [Migration("20220216091308_level2")]
    partial class level2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LearningHubApi2.ViewModel.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("duration")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("enddate")
                        .HasColumnType("datetime2");

                    b.Property<string>("info")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("startdate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentID"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("enrollmentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("Enrollment", (string)null);
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.Trainer", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Trainer", (string)null);
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.User", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("registeredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.Enrollment", b =>
                {
                    b.HasOne("LearningHubApi2.ViewModel.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearningHubApi2.ViewModel.User", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.Trainer", b =>
                {
                    b.HasOne("LearningHubApi2.ViewModel.Course", "Course")
                        .WithMany("Trainer")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearningHubApi2.ViewModel.User", "User")
                        .WithMany("Trainer")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.Course", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("LearningHubApi2.ViewModel.User", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Trainer");
                });
#pragma warning restore 612, 618
        }
    }
}
