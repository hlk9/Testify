using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class ClassUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public byte? Status { get; set; }
    }
}