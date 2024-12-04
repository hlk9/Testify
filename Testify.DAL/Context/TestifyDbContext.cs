using Microsoft.EntityFrameworkCore;
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
            optionsBuilder.UseSqlServer("Data Source=172.188.18.115,1433;Initial Catalog=TestifyDb;TrustServerCertificate=True;User Id=sa; Password=123456789Aa@");
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
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<ExamActivityLog> ExamActivityLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLog>().ToTable("UserLogs");

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
                new Permission { Id = 1, Name = "Quản Trị Viên", Description = "Quyền tối cao, cao nhất của tổ chức, có thể thi hành mọi chức năng của hệ thống", Status = 1 },
                new Permission { Id = 2, Name = "Xem Giảng viên, Khảo thí và Sinh viên", Description = "Xem giảng viên, khảo thí, sinh viên", Status = 1 },
                new Permission { Id = 3, Name = "Chỉnh sửa Giảng viên, Khảo thí và Sinh viên", Description = "Chỉnh sửa giảng viên, khảo thí, sinh viên", Status = 1 },
                new Permission { Id = 4, Name = "Chỉnh sửa và Xoá Giảng viên, Khảo thí và Sinh viên", Description = "Chỉnh sửa và Xoá giảng viên, khảo thí, sinh viên", Status = 1 },
                new Permission { Id = 5, Name = "Xem bài thi, câu hỏi, đáp án", Description = "Xem bài thi, câu hỏi, đáp án", Status = 1 },
                new Permission { Id = 6, Name = "Chỉnh sửa và Xem bài thi, câu hỏi, đáp án", Description = "Chỉnh sửa và Xem bài thi, câu hỏi, đáp án", Status = 1 },
                new Permission { Id = 7, Name = "Chỉnh sửa và Xoá bài thi, câu hỏi, đáp án", Description = "Chỉnh sửa và Xoá bài thi, câu hỏi, đáp án", Status = 1 },
                new Permission { Id = 8, Name = "Xem lớp, môn", Description = "Xem lớp, môn", Status = 1 },
                new Permission { Id = 9, Name = "Chỉnh sửa và Xoá lớp, môn", Description = "Chỉnh sửa và Xoá lớp, môn", Status = 1 },
                new Permission { Id = 10, Name = "Xem bài làm đã nộp", Description = "Xem bài làm đã nộp", Status = 1 },
                new Permission { Id = 11, Name = "Chỉnh sửa và Xoá Xem bài làm đã nộp", Description = "Chỉnh sửa và Xoá Xem bài làm đã nộp", Status = 1 }
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
