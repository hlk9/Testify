namespace Testify.API.DTOs
{
    public class ExamWhitQusetion
    {
        public int Id { get; set; }

        public string ExamName { get; set; }
        //public string? Title { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int? Duration { get; set; }
        public int? NumberOfQuestion { get; set; }
        public int? NumberOfRepeat { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
