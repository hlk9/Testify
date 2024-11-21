using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
