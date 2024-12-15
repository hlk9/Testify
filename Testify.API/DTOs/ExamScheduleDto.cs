namespace Testify.API.DTOs
{
    public class ExamScheduleDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string? Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
