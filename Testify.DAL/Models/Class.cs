 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        [ForeignKey("OrganizationId")]
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        [ForeignKey("SubjectId")]
        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

        public virtual ICollection<ClassUser>? ClassUsers { get; set; } 

        public bool? Status { get; set; } 
    }
}
