using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ClassUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public bool? Status { get; set; } = true;

    }
}
