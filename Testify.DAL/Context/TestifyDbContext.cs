﻿using Microsoft.EntityFrameworkCore;
using Testify.DAL.Models;

namespace Testify.DAL.Context
{
    public class TestifyDbContext : DbContext
    {
        public TestifyDbContext()
        {

        }


        public TestifyDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=57.155.114.218,1433;Initial Catalog=TestifyDb;TrustServerCertificate=True;User Id=sa; Password=123456789Aa@");
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerSubmission> AnswerSubmissions { get; set; }
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
        public DbSet<DoingExam> DoingExams { get; set; }
        //public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<ScoreMethod> ScoreMethods { get; set; }
        public DbSet<ClassExamSchedule> UserExamSchedules { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<ExamDetailQuestion> ExamDetailQuestions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        //public DbSet<BlackListToken> BlackListTokens { get; set; }
        public DbSet<ClassExamSchedule> ClassExamSchedules { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<ExamActivityLog> ExamActivityLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasIndex(c => c.ClassCode).IsUnique();

            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
           .HasIndex(u => u.PhoneNumber).IsUnique();

            modelBuilder.Entity<User>()
          .HasIndex(u => u.UserName).IsUnique();

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


            modelBuilder.Entity<Answer>()
               .HasOne(s => s.Question)
               .WithMany()
               .HasForeignKey(s => s.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionType>()
                .HasData(
                new QuestionType { Id = 1, Name = "Đúng / Sai", Description = "Chọn câu trả lời Đúng hoặc Sai", Status = true },
                new QuestionType { Id = 2, Name = "Chọn đáp án đúng nhất", Description = "Chọn đáp án đúng nhất", Status = true },
                new QuestionType { Id = 3, Name = "Chọn nhiều đáp án đúng", Description = "Chọn các đáp án đúng", Status = true }
                );

            modelBuilder.Entity<Level>()
                .HasData(
                new Level { Id = 1, Name = "Admin", Status = 1 },
                new Level { Id = 2, Name = "Examiner", Status = 1 },
                new Level { Id = 3, Name = "Teacher", Status = 1 },
                new Level { Id = 4, Name = "Student", Status = 1 }
                );

            modelBuilder.Entity<Permission>()
                .HasData(
                new Permission { Id = 1, Name = "Quản lý bài thi", Description = "Quản lý bài thi", Status = 1 },
                new Permission { Id = 2, Name = "Quản lý câu hỏi và đáp án", Description = "Quản lý câu hỏi và đáp án", Status = 1 },
                new Permission { Id = 3, Name = "Quản lý môn học", Description = "Quản lý môn học", Status = 1 },
                new Permission { Id = 4, Name = "Quản lý lịch thi", Description = "Quản lý lịch thi", Status = 1 }
                );
            modelBuilder.Entity<User>()
               .HasData(
                new User { Id = Guid.NewGuid(), Address = "A", FullName = "Nguyen Van A", UserName = "nva", DateOfBirth = DateTime.Now, PhoneNumber = "0987654321", Email = "abcde@gmail.com", PasswordHash = "4297f44b13955235245b2497399d7a93", AvatarUrl = null, LastLogin = null, Status = 1, LevelId = 4, Sex = false },
                new User { Id = Guid.NewGuid(), Address = "A", FullName = "Nguyen Van B", UserName = "nvb", DateOfBirth = DateTime.Now, PhoneNumber = "0987654322", Email = "abscde@gmail.com", PasswordHash = "4297f44b13955235245b2497399d7a93", AvatarUrl = null, LastLogin = null, Status = 1, LevelId = 3, Sex = true },
                new User { Id = Guid.NewGuid(), Address = "A", FullName = "Nguyen Van C", UserName = "nvc", DateOfBirth = DateTime.Now, PhoneNumber = "0987254322", Email = "aabscde@gmail.com", PasswordHash = "4297f44b13955235245b2497399d7a93", AvatarUrl = null, LastLogin = null, Status = 1, LevelId = 2, Sex = true },
                new User { Id = Guid.NewGuid(), Address = "A", FullName = "Nguyen Van D", UserName = "nvd", DateOfBirth = DateTime.Now, PhoneNumber = "0287654322", Email = "absscde@gmail.com", PasswordHash = "4297f44b13955235245b2497399d7a93", AvatarUrl = null, LastLogin = null, Status = 1, LevelId = 1, Sex = true }
               );

            modelBuilder.Entity<Subject>()
                .HasData(
                new Subject { Id = 1, Description = "None", Name = "Ly", Status = 1 }
                );
        }
    }
}
