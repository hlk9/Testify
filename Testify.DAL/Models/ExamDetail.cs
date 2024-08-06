using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamDetail
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Code { get; set; }
        public bool? Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UpdateBy { get; set; } 
    }
}
