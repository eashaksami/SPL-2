﻿// <auto-generated />
using System;
using EBET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EBET.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210113022135_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("JWTApi.Models.Chapter", b =>
                {
                    b.Property<int>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourseCode")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ChapterId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("ImageId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("JWTApi.Models.Course", b =>
                {
                    b.Property<int>("CourseCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CourseCode");

                    b.HasIndex("ImageId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("JWTApi.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("JWTApi.Models.PracticeExam", b =>
                {
                    b.Property<int>("PracticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourseCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalCorrectAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalWrongAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PracticeId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("UserId");

                    b.ToTable("PracticeExams");
                });

            modelBuilder.Entity("JWTApi.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnswerDetails")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ChapterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Option1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Option2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Option3")
                        .HasColumnType("TEXT");

                    b.Property<string>("Option4")
                        .HasColumnType("TEXT");

                    b.Property<string>("question")
                        .HasColumnType("TEXT");

                    b.HasKey("QuestionId");

                    b.HasIndex("ChapterId");

                    b.HasIndex("ImageId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("JWTApi.Models.QuestionStatus", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PCorrectOrWrong")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PSeenOrUnseen")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TCorrectOrWrong")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TSeenOrUnseen")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "QuestionId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionStatuses");
                });

            modelBuilder.Entity("JWTApi.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourseCode")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Length")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SubscriberId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("JWTApi.Models.TestExam", b =>
                {
                    b.Property<int>("TestExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourseCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalCorrectAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalWrongAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TestExamId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("UserId");

                    b.ToTable("TestExams");
                });

            modelBuilder.Entity("JWTApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JWTApi.Models.Chapter", b =>
                {
                    b.HasOne("JWTApi.Models.Course", "Course")
                        .WithMany("Chapter")
                        .HasForeignKey("CourseCode");

                    b.HasOne("JWTApi.Models.Image", "Image")
                        .WithMany("Chapters")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("JWTApi.Models.Course", b =>
                {
                    b.HasOne("JWTApi.Models.Image", "Image")
                        .WithMany("Courses")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("JWTApi.Models.PracticeExam", b =>
                {
                    b.HasOne("JWTApi.Models.Course", "Course")
                        .WithMany("PracticeExams")
                        .HasForeignKey("CourseCode");

                    b.HasOne("JWTApi.Models.User", "User")
                        .WithMany("PracticeExam")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JWTApi.Models.Question", b =>
                {
                    b.HasOne("JWTApi.Models.Chapter", "Chapter")
                        .WithMany("Questions")
                        .HasForeignKey("ChapterId");

                    b.HasOne("JWTApi.Models.Image", "Image")
                        .WithMany("Questions")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("JWTApi.Models.QuestionStatus", b =>
                {
                    b.HasOne("JWTApi.Models.Course", "Course")
                        .WithMany("QuestionStatuses")
                        .HasForeignKey("CourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWTApi.Models.Question", "Question")
                        .WithMany("QuestionStatuses")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWTApi.Models.User", "User")
                        .WithMany("QuestionStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTApi.Models.Subscription", b =>
                {
                    b.HasOne("JWTApi.Models.Course", "Course")
                        .WithMany("Subscriptions")
                        .HasForeignKey("CourseCode");

                    b.HasOne("JWTApi.Models.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JWTApi.Models.TestExam", b =>
                {
                    b.HasOne("JWTApi.Models.Course", "Course")
                        .WithMany("TestExams")
                        .HasForeignKey("CourseCode");

                    b.HasOne("JWTApi.Models.User", "User")
                        .WithMany("TestExams")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}