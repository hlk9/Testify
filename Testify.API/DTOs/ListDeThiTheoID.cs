namespace Testify.API.DTOs
{
    public class ListDeThiTheoID // List  DeThi Theo IDEXAM
    {
        public int Id { get; set; }
        public string? NameDethi { get; set; }    
        public int? SubjectId { get; set; } //Lay ra Môn
        public int? QuestionLevelId { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; } //Người tạo
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; } //Người Sửa
        public int? SLCHTrongDethiTheoMD { get; set; }
        public byte? Status { get; set; }
        public double? DiemMoiCau { get; set; }
        public string? Name_MucDoCH {  get; set; }
        public string? SubjectName { get; set; }

    }
}
