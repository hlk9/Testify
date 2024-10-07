using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }        
        public string Code { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UpdateBy { get; set; } 
    }
}
