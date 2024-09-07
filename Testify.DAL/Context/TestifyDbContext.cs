﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Models;

namespace Testify.DAL.Context
{
    public class TestifyDbContext:DbContext
    {
        public TestifyDbContext()
        {
            
        }

        public TestifyDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=20.5.250.222,1433;Initial Catalog=TestifyDb;TrustServerCertificate=True;User Id=sa; Password=123456789Aa@");
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerSubmission> AnswerSubmissions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<QuestionLevel> QuestionLevels { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassUser> ClassUsers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamDetail> ExamDetails { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; } 
        public DbSet<ScoreMethod> ScoreMethods { get; set; }
        public DbSet<UserExamSchedule> UserExamSchedules { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<ExamDetailQuestion> ExamDetailQuestions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Class>()
                .HasIndex(c => c.ClassCode).IsUnique();

            modelBuilder.Entity<Organization>()
                .HasIndex(c => c.OrganizationCode).IsUnique();

            modelBuilder.Entity<Submission>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.ExamDetail)
                .WithMany()
                .HasForeignKey(s => s.ExamDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.ExamSchedule)
                .WithMany()
                .HasForeignKey(s => s.ExamScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Organization)
                .WithMany()
                .HasForeignKey(s => s.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
               .HasOne(s => s.Question)
               .WithMany()
               .HasForeignKey(s => s.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionType>()
                .HasData(                
                new QuestionType { Id = 1,Name="Đúng / Sai",Description="Chọn câu trả lời Đúng hoặc Sai", Status=true},
                new QuestionType { Id = 2, Name = "Chọn đáp án đúng nhất", Description = "Chọn đáp án đúng nhất", Status = true },
                new QuestionType { Id = 3, Name = "Chọn nhiều đáp án đúng", Description = "Chọn các đáp án đúng", Status = true },
                new QuestionType { Id = 4, Name = "Nhập đáp án đúng", Description = "Nhập câu trả lời", Status = true }
                );

            modelBuilder.Entity<Level>()
                .HasData(
                new Level { Id = 1, Name = "Admin", Status = true  },
                new Level { Id = 2, Name = "Examiner", Status = true },
                new Level { Id = 3, Name = "Teacher", Status = true },
                new Level { Id = 4, Name = "Student", Status = true }
                );

        }
    }
}
